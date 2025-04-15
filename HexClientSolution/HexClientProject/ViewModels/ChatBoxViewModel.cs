using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using HexClientProject.Models;
using ReactiveUI;

namespace HexClientProject.ViewModels;

public class ChatBoxViewModel : ReactiveObject
{
    private readonly StateManager _stateManager = StateManager.Instance;

    private ObservableCollection<MessageModel> Messages { get; } = new();

    public ObservableCollection<ChatScope> Scopes { get; } = new()
    {
        ChatScope.Global,
        ChatScope.Party,
        ChatScope.Whisper,
        ChatScope.Guild,
        // ChatScope.System // Not supposed to chat with the system right ?
    };
    public ObservableCollection<string> FilteringScopes { get; } = new()
    {
        "Global",
        "Party",
        "Whisper",
        "Guild",
        "System"
    };
    
    private ChatScope _selectedScope = ChatScope.Global;
    public ChatScope SelectedScope
    {
        get => _selectedScope;
        set => this.RaiseAndSetIfChanged(ref _selectedScope, value);
    }

    public bool CheckForScopeCommand(TextBox textBox)
    {
        if (string.IsNullOrEmpty(textBox.Text))
            return false;
        bool changedScope = false;
        string pattern = @"^/(g|p|mp$|r|all)\b";

        var match = Regex.Match(MessageInput, pattern);
        if (match.Success)
        {
            string channel = match.Groups[1].Value; // "g", "p", "mp", "all"
            switch (channel)
            {
                case "g":
                    SelectedScope = ChatScope.Guild;
                    textBox.CaretIndex = textBox.Text!.Length;
                    changedScope = true;
                    break;
                case "p":
                    SelectedScope = ChatScope.Party;
                    textBox.CaretIndex = textBox.Text!.Length;
                    changedScope = true;
                    break;
                case "mp":
                    // SelectedScope = ChatScope.Whisper;
                    string messageToAdd = Regex.Replace(MessageInput, "^/mp", "");
                    MessageInput = $"/mp <>" + messageToAdd;
                    textBox.CaretIndex = 5;
                    changedScope = true;
                    break;
                case "r":
                    // SelectedScope = ChatScope.Whisper;
                    messageToAdd = Regex.Replace(MessageInput, "^/r", "");
                    MessageInput = $"/mp <{SelectedWhisperTarget}>" + messageToAdd; 
                    textBox.CaretIndex = textBox.Text!.Length;
                    changedScope = true;
                    break;
                case "all":
                    SelectedScope = ChatScope.Global;
                    textBox.CaretIndex = textBox.Text!.Length;
                    changedScope = true;
                    break;
            }

            // Remove the command from the message
            if (SelectedScope != ChatScope.Whisper)
            {
                MessageInput = Regex.Replace(MessageInput, pattern, "").Trim();
            }
        }
        return changedScope;
    }

    private string _messageInput = string.Empty;

    public string MessageInput
    {
        get => _messageInput;
        set => this.RaiseAndSetIfChanged(ref _messageInput, value);
    }

    private string? _selectedWhisperTarget;
    public string? SelectedWhisperTarget
    {
        get => _selectedWhisperTarget;
        set => this.RaiseAndSetIfChanged(ref _selectedWhisperTarget, value);
    }
    private string _selectedFilter = "Global";

    public string SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            if (FilteringScopes.Contains(value) == false && !string.IsNullOrEmpty(value))
                FilteringScopes.Add(value);
            this.RaiseAndSetIfChanged(ref _selectedFilter, value);
            ApplyFilter(); // Update the displayed messages
        }
    }

    public ObservableCollection<MessageModel> FilteredMessages { get; } = new();

    private void ApplyFilter()
    {
        FilteredMessages.Clear();

        IEnumerable<MessageModel> filtered = Messages;
        switch (SelectedFilter)
        {
            case "Global":
                break;
            case "Party":
                filtered = filtered.Where(msg => msg.Scope is ChatScope.Party or ChatScope.System);
                break;
            case "Whisper":
                filtered = filtered.Where(msg =>(msg.Scope is ChatScope.Whisper or ChatScope.System));
                break;
            case "Guild":
                filtered = filtered.Where(msg => msg.Scope is ChatScope.Guild or ChatScope.System);
                break;
            case "System":
                filtered = filtered.Where(msg => msg.Scope is ChatScope.System);
                break;
            default:
                filtered = filtered.Where(msg => (msg.Scope is ChatScope.Whisper &&
                                                  (msg.Sender == SelectedFilter || msg.WhisperingTo == SelectedFilter)
                                                 || msg.Scope == ChatScope.System));
                break;
        }
        foreach (var msg in filtered)
            FilteredMessages.Add(msg);
    }
    
    public void ApplyFilterToWhisper(string username)
    {
        SelectedFilter = username;
        FilteredMessages.Clear();
        var filtered = Messages.Where(msg =>
            (msg.Scope == ChatScope.System) || (msg.Scope == ChatScope.Whisper &&
                                                (
                                                    (msg.Sender == username && msg.WhisperingTo ==
                                                        _stateManager.SummonerInfo.GameName)
                                                    || msg.Sender == _stateManager.SummonerInfo.GameName &&
                                                    msg.WhisperingTo == username)));
        foreach (var msg in filtered)
        {
            FilteredMessages.Add(msg);
        }
    }
    private bool ParseWhisper(string input) // Returns true if the input is a whisper command
    {
        var match = Regex.Match(input, @"^/mp\s+<([^>\s]+)>\s*(.*)");
        if (match.Success)
        {
            SelectedWhisperTarget = match.Groups[1].Value;
            MessageInput = match.Groups[2].Value;
            return true;
        }

        if (Regex.Match(input, @"^/mp\s+<>").Success)
        {
            SelectedWhisperTarget = "";
        }
        MessageInput = input;
        return false;
    }
    
    private void Messages_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            ApplyFilter(); // Display Messages each time a new message is added
        }
    }
    public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }
    public ChatBoxViewModel()
    {
        Messages.CollectionChanged += Messages_CollectionChanged;
        SelectedScope = ChatScope.Global;
        SendMessageCommand = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrWhiteSpace(MessageInput))
                return;
            bool isWhisper = ParseWhisper(MessageInput); // Extract the whisper target only if Whisper Scope is selected else set to empty
            if (isWhisper && string.IsNullOrWhiteSpace(SelectedWhisperTarget))
            {
                SendSystemMessage("Please enter a username.");
                return;
            }
            if (SelectedScope == ChatScope.Whisper && (!isWhisper ||
                                                       _stateManager.Friends.FirstOrDefault(f =>
                                                           f.Username == SelectedWhisperTarget) == null))
            {
                if (!isWhisper)
                    SendSystemMessage("Please enter a proper whisper command. Usage: /mp <username>");
                else
                    SendSystemMessage(string.IsNullOrWhiteSpace(SelectedWhisperTarget)
                        ? "Please enter a username."
                        : $"Username: {SelectedWhisperTarget} not found.");
                MessageInput = $"/mp <{SelectedWhisperTarget}> ";
                return;
            }
            MessageInput = Regex.Replace(MessageInput, @"^/(g|p|mp\s+<>) ", "");
            ChatScope msgScope = SelectedScope;
            if (isWhisper)
                msgScope = ChatScope.Whisper;
            Messages.Add(new MessageModel
            {
                Sender = _stateManager.SummonerInfo.GameName,
                Content = MessageInput,
                Scope = msgScope,
                Timestamp = DateTime.Now,
                WhisperingTo = SelectedWhisperTarget
            });
            MessageInput = string.Empty;
            ApplyFilter();
        });
    }

    private void SendSystemMessage(string message)
    {
        message = "[" + message + "]";
        Messages.Add(new MessageModel
        {
            Sender = "System",
            Content = message,
            Scope = ChatScope.System,
            Timestamp = DateTime.Now
        });
    }
}