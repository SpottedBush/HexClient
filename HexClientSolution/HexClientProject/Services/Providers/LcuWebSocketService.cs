using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HexClientProject.Services.Providers;
public class LcuWebSocketService
{
    private static LcuWebSocketService? _instance;
    private static readonly Lock Lock = new();

    private readonly ClientWebSocket _webSocket = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly ConcurrentDictionary<string, List<Action<JsonElement>>> _listeners = new();
    private readonly Uri _uri;
    private readonly string _auth;

    private LcuWebSocketService(string socketUri, string base64Auth)
    {
        _uri = new Uri(socketUri);
        _auth = base64Auth;
    }

    public static LcuWebSocketService Instance(string socketUri, string base64Auth)
    {
        lock (Lock)
        {
            return _instance ??= new LcuWebSocketService(socketUri, base64Auth);
        }
    }

    public async Task ConnectAsync()
    {
        if (_webSocket.State == WebSocketState.Open) return;

        _webSocket.Options.SetRequestHeader("Authorization", $"Basic {_auth}");
        _webSocket.Options.RemoteCertificateValidationCallback = (_, _, _, _) => true;

        await _webSocket.ConnectAsync(_uri, _cts.Token);
        _ = Task.Run(ReceiveLoopAsync);
    }

    public void RegisterListener(string endpoint, Action<JsonElement> callback)
    {
        _listeners.AddOrUpdate(
            endpoint,
            _ => [callback],
            (_, list) =>
            {
                lock (list)
                {
                    list.Add(callback);
                    return list;
                }
            }
        );
    }

    public async Task SubscribeAsync(string endpoint)
    {
        var message = new
        {
            eventType = "subscribe",
            uri = endpoint
        };

        string json = JsonSerializer.Serialize(message);
        byte[] buffer = Encoding.UTF8.GetBytes(json);
        await _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cts.Token);
    }

    private async Task ReceiveLoopAsync()
    {
        var buffer = new byte[8192];

        while (!_cts.Token.IsCancellationRequested && _webSocket.State == WebSocketState.Open)
        {
            var segment = new ArraySegment<byte>(buffer);
            WebSocketReceiveResult result;

            using var ms = new MemoryStream();
            do
            {
                result = await _webSocket.ReceiveAsync(segment, _cts.Token);
                ms.Write(segment.Array!, segment.Offset, result.Count);
            }
            while (!result.EndOfMessage);

            string json = Encoding.UTF8.GetString(ms.ToArray());
            HandleWebSocketMessage(json);
        }
    }

    private void HandleWebSocketMessage(string rawJson)
    {
        try
        {
            using var doc = JsonDocument.Parse(rawJson);
            var root = doc.RootElement;

            if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() < 3)
                return;

            string eventUri = root[2].GetProperty("uri").GetString() ?? string.Empty;
            JsonElement eventData = root[2].GetProperty("data");

            if (_listeners.TryGetValue(eventUri, out var handlers))
            {
                lock (handlers)
                {
                    foreach (var handler in handlers)
                        handler.Invoke(eventData);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WebSocket] Failed to handle message: {ex.Message}");
        }
    }

    public async Task DisconnectAsync()
    {
        await _cts.CancelAsync();
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
        }
    }
}