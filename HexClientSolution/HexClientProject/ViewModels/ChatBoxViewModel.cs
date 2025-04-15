using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using DynamicData.Binding;
using HexClientProject.Models;
using ReactiveUI;

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
    public ObservableCollection<ChatScope> FilteringScopes { get; } = new()
    {
        ChatScope.Global,
        ChatScope.Party,
        ChatScope.Whisper,
        ChatScope.Guild,
        ChatScope.System
    };
    
    private ChatScope _selectedScope = ChatScope.Global;
    public ChatScope SelectedScope
    {
        get => _selectedScope;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedScope, value);
            switch (value)
            {
                case ChatScope.System:
                    break;
                case ChatScope.Whisper:
                    string messageToAdd;
                    if (Regex.Match(MessageInput, "^/mp").Success)
                    {
                        messageToAdd = Regex.Replace(MessageInput, "/mp", "");
                        MessageInput = $"/mp <>" + messageToAdd;
                        
                    }
                    else if (Regex.Match(MessageInput, "^/r").Success)
                    {
                        messageToAdd = Regex.Replace(MessageInput, "/r", "");
                        MessageInput = $"/mp <{SelectedWhisperTarget}>" + messageToAdd; 
                    }
                    break;
                case ChatScope.Guild:
                    // MessageInput = "";
                    break;
                case ChatScope.Party:
                    // MessageInput = "";
                    break;
                case ChatScope.Global:
                    // MessageInput = "";
                    break;
                default:
                    MessageInput = string.Empty;
                    break;
            }
        }
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
                    SelectedScope = ChatScope.Whisper;
                    textBox.CaretIndex = 5;
                    changedScope = true;
                    break;
                case "r":
                    SelectedScope = ChatScope.Whisper;
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

    private string _selectedWhisperTarget;
    public string SelectedWhisperTarget
    {
        get => _selectedWhisperTarget;
        set => this.RaiseAndSetIfChanged(ref _selectedWhisperTarget, value);
    }
    private ChatScope _selectedFilter = ChatScope.Global;

    public ChatScope SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedFilter, value);
            ApplyFilter(); // call a method that updates FilteredMessages
        }
    }

    public ObservableCollection<MessageModel> FilteredMessages { get; } = new();

    private void ApplyFilter()
    {
        FilteredMessages.Clear();

        var filtered = SelectedFilter == ChatScope.Global
            ? Messages
            : Messages.Where(msg => (msg.Scope == SelectedFilter));

        foreach (var msg in filtered)
            FilteredMessages.Add(msg);
    }
    
    public void ApplyFilterToWhisper(string username)
    {
        FilteredMessages.Clear();
        var filtered = Messages.Where(msg =>
            msg.Scope == SelectedFilter && 
            (msg.Sender == username 
             || msg.WhisperingTo == username));
        foreach (var msg in filtered)
            FilteredMessages.Add(msg);
    }
    public ReactiveCommand<TextBox, Unit> SendMessageCommand { get; }
    private string ExtractWhisperTarget(string input)
    {
        if (SelectedScope == ChatScope.Whisper)
        {
            var match = Regex.Match(input, @"^/mp\s+<([^>\s]+)>\s*(.*)");
            if (match.Success)
            {
                SelectedWhisperTarget = match.Groups[1].Value;
                return match.Groups[2].Value;
            }

            if (Regex.Match(input, @"^/mp\s+<>").Success)
            {
                SelectedWhisperTarget = "";
            }
        }
        return input; // Return the input if not whispering
    }
    
    private void Messages_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            ApplyFilter(); // Display Messages each time a new message is added
        }
    }
    public ChatBoxViewModel()
    {
        Messages.CollectionChanged += Messages_CollectionChanged;
        SelectedScope = ChatScope.Global;
        SendMessageCommand = ReactiveCommand.Create((TextBox textBlock) =>
        {
            if (string.IsNullOrWhiteSpace(MessageInput))
                return;
            MessageInput = ExtractWhisperTarget(MessageInput); // Extract the whisper target only if Whisper Scope is selected else set to empty
            if (SelectedScope == ChatScope.Whisper && (string.IsNullOrWhiteSpace(SelectedWhisperTarget) ||
                                                       _stateManager.Friends.FirstOrDefault(f =>
                                                           f.Username == SelectedWhisperTarget) == null))
            {
                SendSystemMessage(string.IsNullOrWhiteSpace(SelectedWhisperTarget)
                    ? "Please enter a username."
                    : $"Username: {SelectedWhisperTarget} not found.");
                MessageInput = $"/mp <{SelectedWhisperTarget}> ";
                return;
            }
            MessageInput = Regex.Replace(MessageInput, @"^/(g|p|mp) ", "");
            Messages.Add(new MessageModel
            {
                Sender = _stateManager.SummonerInfo.GameName,
                Content = MessageInput,
                Scope = SelectedScope,
                Timestamp = DateTime.Now,
                WhisperingTo = SelectedWhisperTarget
            });
            switch (SelectedScope)
            {
                case ChatScope.Whisper:
                    MessageInput = "/mp <> ";
                    // textBlock.CaretIndex = 5;
                    break;
                default:
                    MessageInput = string.Empty;
                    break;
            }
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