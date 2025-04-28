using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using ReactiveUI;

namespace HexClientProject.ViewModels;

public class ChatBoxViewModel : ReactiveObject
{
    private readonly GlobalStateManager _globalStateManager = GlobalStateManager.Instance;
    private readonly SocialStateManager _socialStateManager = SocialStateManager.Instance; 


    public ObservableCollection<MessageModel> Messages { get; } = new();
    public ObservableCollection<ChatScope> Scopes { get; } =
    [
        ChatScope.Global,
        ChatScope.Party,
        ChatScope.Whisper,
        ChatScope.Guild
        // ChatScope.System // Not supposed to chat with the system, right?
    ];
    public ObservableCollection<string> FilteringScopes { get; } =
    [
        "Global",
        "Party",
        "Whisper",
        "Guild",
        "System"
    ];
    
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
    public string? SelectedWhisperTarget // GameNameTag
    {
        get => _selectedWhisperTarget;
        set => this.RaiseAndSetIfChanged(ref _selectedWhisperTarget, value);
    }
    private string _selectedFilter = "Global";

    public string SelectedFilter // if Whisper => GameNameTag
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

    public ObservableCollection<MessageModel> FilteredMessages { get; } = [];

    private void ApplyFilterToLastMessage()
    {
        var lastMessage = Messages.LastOrDefault();
        // Message exists and is not in the mutedUsers
        if (lastMessage != null && !_socialStateManager.MutedUsernames.Contains(lastMessage.Sender))
        {
            if (lastMessage.Scope == ChatScope.System)
            {
                FilteredMessages.Add(lastMessage);
                return;
            }
            // Message is not whisper, then SelectedFilter must correspond to the Scope.
            if (lastMessage.Scope != ChatScope.Whisper && lastMessage.Scope.ToString() != SelectedFilter) return;
            // Message is whisper, then SelectedFilter must correspond to a username related to the message.
            if (lastMessage.Scope == ChatScope.Whisper && 
                lastMessage.Sender != SelectedFilter && lastMessage.WhisperingTo != SelectedFilter) return;
            FilteredMessages.Add(lastMessage);
        }
    }
    
    private void ApplyFilter()
    {
        FilteredMessages.Clear();
        IEnumerable<MessageModel> filtered = Messages.Where( // Removes muted Users
            messageModel => _socialStateManager.MutedUsernames.All(mutedUser=> messageModel.Sender != mutedUser));
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
    
    public void ApplyFilterToWhisper(string gameNameTag)
    {
        SelectedFilter = gameNameTag;
        FilteredMessages.Clear();
        IEnumerable<MessageModel> filtered = Messages.Where( // Removes muted Users
            messageModel => _socialStateManager.MutedUsernames.All(mutedUser=> messageModel.Sender != mutedUser));
        
        filtered = filtered.Where(msg =>
            (msg.Scope == ChatScope.System) || (msg.Scope == ChatScope.Whisper &&
                                                (
                                                    (msg.Sender == gameNameTag && msg.WhisperingTo ==
                                                        _globalStateManager.SummonerInfo.GameName)
                                                    || msg.Sender == _globalStateManager.SummonerInfo.GameName &&
                                                    msg.WhisperingTo == gameNameTag)));
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
            ApplyFilterToLastMessage(); // Display the new message
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
                                                       _socialStateManager.Friends.FirstOrDefault(f =>
                                                           f.GameNameTag == SelectedWhisperTarget) == null))
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
            ApiProvider.SocialService.SendMessage(new MessageModel
            {
                Sender = _globalStateManager.SummonerInfo.GameNameTag,
                Content = MessageInput,
                Scope = msgScope,
                Timestamp = DateTime.Now,
                WhisperingTo = SelectedWhisperTarget
            });
            MessageInput = string.Empty;
            ApplyFilter();
        });
        Messages.Add(new MessageModel
        {
            Sender = "ouistiti#EUWW",
            Content = "test2",
            Scope = ChatScope.Global,
            Timestamp = DateTime.Now
        });
    }

    public void SendSystemMessage(string message)
    {
        message = "[" + message + "]";
        ApiProvider.SocialService.SendMessage(new MessageModel
        {
            Sender = "System",
            Content = message,
            Scope = ChatScope.System,
            Timestamp = DateTime.Now
        });
    }
}