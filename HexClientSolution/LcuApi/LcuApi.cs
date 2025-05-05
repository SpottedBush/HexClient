using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace LcuApi;
public class LeagueClient : ILeagueClient
{
    private string _apiUri = null!;

    private string _leaguePath = null!;

    private int _leaguePid;

    private readonly HttpClient _httpClient;

    private readonly RuneManager _runeManager;

    private readonly Summoners _summoners;

    private string Token { get; set; } = null!;

    private ushort Port { get; set; }

    public event LeagueClosedHandler LeagueClosed = null!;

    public LeagueClient()
    {
        _httpClient = new HttpClient(new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        });
        _runeManager = new RuneManager(this);
        _summoners = new Summoners(this);
    }

    public static async Task<ILeagueClient> Connect()
    {
        Process? process = await FindLeagueAsync();
        LeagueClient obj = (LeagueClient)(await Connect(Path.GetDirectoryName(process?.MainModule?.FileName) ?? string.Empty));
        if (process != null) obj._leaguePid = process.Id;
        return obj;
    }

    private static async Task<ILeagueClient> Connect(string path)
    {
        LeagueClient ret = new LeagueClient
        {
            _leaguePath = path
        };
        await ret.WatchLockFileAsync();
        Process processById = Process.GetProcessById(ret._leaguePid);
        processById.EnableRaisingEvents = true;
        processById.Exited += ret.League_Exited!;
        return ret;
    }

    private async Task WatchLockFileAsync()
    {
        string lockFilePath = Path.Combine(_leaguePath, "lockfile");
        if (File.Exists(lockFilePath))
        {
            await ParseLockFileAsync(lockFilePath);
            return;
        }

        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        FileSystemWatcher watcher = new FileSystemWatcher(_leaguePath);
        FileSystemEventHandler? handler = null;
        handler = delegate (object s, FileSystemEventArgs e)
        {
            if (e.Name != null && !e.Name.Equals("lockfile")) return;
            _ = ParseLockFileAsync(lockFilePath);
            tcs.TrySetResult(result: true);
            watcher.Created -= handler;
            watcher.Dispose();
        };
        watcher.Created += handler;
        watcher.EnableRaisingEvents = true;
    }

    private async Task ParseLockFileAsync(string lockFile)
    {
        await using FileStream fileStream = new FileStream(lockFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using StreamReader reader = new StreamReader(fileStream);
        string[] array = (await reader.ReadToEndAsync()).Split([':']);
        Token = array[3];
        Port = ushort.Parse(array[2]);
        _apiUri = "https://127.0.0.1:" + Port + "/";
        Encoding.ASCII.GetBytes("riot:" + Token);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{Token}")));
        _httpClient.BaseAddress = new Uri(_apiUri);
        if (_leaguePid == 0)
        {
            _leaguePid = int.Parse(array[1]);
        }
    }

    private static async Task<Process?> FindLeagueAsync()
    {
        Process? ret = null;
        await Task.Run(delegate
        {
            Process?[] processesByName;
            while (true)
            {
                processesByName = Process.GetProcessesByName("LeagueClientUx");
                if (processesByName.Length != 0)
                {
                    break;
                }

                Thread.Sleep(1000);
            }

            ret = processesByName[0];
        });
        return ret;
    }

    private void League_Exited(object sender, EventArgs e)
    {
        LeagueClosed();
    }

    public RuneManager GetRuneManager()
    {
        return _runeManager;
    }

    public HttpClient GetHttpClient()
    {
        return _httpClient;
    }

    public async Task<HttpResponseMessage> MakeApiRequest(HttpMethod method, string endpoint, object? data = null)
    {
        string content = ((data == null) ? "" : JsonConvert.SerializeObject(data));
        return method switch
        {
            HttpMethod.Get => await _httpClient.GetAsync(endpoint),
            HttpMethod.Post => await _httpClient.PostAsync(endpoint,
                new StringContent(content, Encoding.UTF8, "application/json")),
            HttpMethod.Put => await _httpClient.PutAsync(endpoint,
                new StringContent(content, Encoding.UTF8, "application/json")),
            HttpMethod.Delete => await _httpClient.DeleteAsync(endpoint),
            _ => throw new Exception("Unsupported HTTP method")
        };
    }


    public async Task<T> MakeApiRequestAs<T>(HttpMethod method, string endpoint, object? data = null)
    {
        return JsonConvert.DeserializeObject<T>(await (await MakeApiRequest(method, endpoint, data)).Content.ReadAsStringAsync())!;
    }

    public Summoners GetSummonersModule()
    {
        return _summoners;
    }
}