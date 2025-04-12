using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using DynamicData.Binding;
using HexClientProject.Models;
using ReactiveUI;

public class ChatBoxViewModel : ReactiveObject
{
    StateManager _stateManager = StateManager.Instance;
    public ObservableCollection<MessageModel> Messages { get; } = new();
    public ObservableCollection<ChatScope> Scopes { get; } = new ObservableCollection<ChatScope>
    {
        ChatScope.Global,
        ChatScope.Party,
        ChatScope.Whisper,
        ChatScope.Guild,
        // ChatScope.System // Not supposed to chat with the system right ?
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
                    return;
                case ChatScope.Whisper:
                    IsWhisperScopeSelected = true;
                    MessageInput = "/mp <>";
                    break;
                case ChatScope.Guild:
                    MessageInput = "/g";
                    break;
                case ChatScope.Party:
                    MessageInput = "/p";
                    break;
                default:
                    IsWhisperScopeSelected = false;
                    MessageInput = string.Empty;
                    break;
            }
        }
    }
    
    public ReadOnlyObservableCollection<FriendModel>? Friends { get; }

    private string _messageInput;
    public string MessageInput
    {
        get => _messageInput;
        set => this.RaiseAndSetIfChanged(ref _messageInput, value);
    }
    public ObservableCollection<string> FilterOptions { get; } = new()
    {
        "Global", "Party", "MP", "System", "Guild"
    };

    private string _selectedWhisperTarget;
    public string SelectedWhisperTarget
    {
        get => _selectedWhisperTarget;
        set => this.RaiseAndSetIfChanged(ref _selectedWhisperTarget, value);
    }
    private string _selectedFilter = "Global";
    private bool _isWhisperScopeSelected;
    public bool IsWhisperScopeSelected
    {
        get => _isWhisperScopeSelected;
        set => this.RaiseAndSetIfChanged(ref _isWhisperScopeSelected, value);
    }
    public string SelectedFilter
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

        var filtered = SelectedFilter == "Global"
            ? Messages
            : Messages.Where(msg => (
            SelectedFilter == "MP" && msg.Scope.ToString() == "Whisper"
            || msg.Scope.ToString() == SelectedFilter));

        foreach (var msg in filtered)
            FilteredMessages.Add(msg);
    }

    public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }
    private string ExtractWhisperTarget(string input)
    {
        SelectedWhisperTarget = string.Empty;
        if (SelectedScope == ChatScope.Whisper)
        {
            var match = Regex.Match(input, @"/mp\s+<([^>\s]+)>\s*(.*)");
            if (match.Success)
            {
                SelectedWhisperTarget = match.Groups[1].Value;
                return match.Groups[2].Value;
            }
        }
        return input; // Return the input if not whispering
    }
    public ChatBoxViewModel()
    {
        Friends = (ReadOnlyObservableCollection<FriendModel>?)_stateManager.Friends;
        SelectedScope = ChatScope.Global;
        SendMessageCommand = ReactiveCommand.Create(() =>
        {
            Console.WriteLine("Message sent");
            MessageInput = ExtractWhisperTarget(MessageInput); // Extract the whisper target only if Whisper Scope is selected else set to empty
            if (SelectedScope == ChatScope.Whisper && string.IsNullOrWhiteSpace(SelectedWhisperTarget))
                return;
            MessageInput = Regex.Replace(MessageInput, @"^/(g|p|mp) ", "");
            Messages.Add(new MessageModel
            {
                Content = MessageInput,
                Scope = SelectedScope,
                Timestamp = DateTime.Now,
                WhisperingTo = SelectedWhisperTarget
            });
            switch (SelectedScope)
            {
                case ChatScope.Whisper:
                    MessageInput = $"/mp <{SelectedWhisperTarget}> ";
                    break;
                case ChatScope.Guild:
                    MessageInput = "/g ";
                    break;
                case ChatScope.Party:
                    MessageInput = "/p ";
                    break;
                default:
                    IsWhisperScopeSelected = false;
                    MessageInput = string.Empty;
                    break;
            }
            ApplyFilter();
        });
    }
}