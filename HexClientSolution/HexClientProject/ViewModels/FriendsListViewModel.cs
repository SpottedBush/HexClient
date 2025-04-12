using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using HexClientProject.Models;
using HexClientProject.ViewModels;
using ReactiveUI;

public class FriendsListViewModel : ViewModelBase
{
    public ObservableCollection<FriendModel> Friends { get; } = new();
    public ObservableCollection<MessageModel> ChatMessages { get; } = new();

    private readonly StateManager _stateManager = StateManager.Instance;
    private string _newMessageText;
    public string NewMessageText
    {
        get => _newMessageText;
        set => this.RaiseAndSetIfChanged(ref _newMessageText, value);
    }

    public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }
    
    public ReactiveCommand<Unit, Unit> CloseChatCommand { get; }

    public ReactiveCommand<Unit, Unit> AddFriendCommand { get; }
    public string NewFriendUsername { get; set; }
    private FriendModel _selectedFriend;

    public FriendModel SelectedFriend
    {
        get => _selectedFriend;
        set => this.RaiseAndSetIfChanged(ref _selectedFriend, value);
    }

    private FriendModel? _selectedChatFriend;
    public FriendModel? SelectedChatFriend
    {
        get => _selectedChatFriend;
        set => this.RaiseAndSetIfChanged(ref _selectedChatFriend, value);
    }

    private bool _isChatOpen;
    public bool IsChatOpen
    {
        get => _isChatOpen;
        set => this.RaiseAndSetIfChanged(ref _isChatOpen, value);
    }
    
    public FriendsListViewModel()
    {
        AddFriendCommand = ReactiveCommand.CreateFromTask(AddFriendAsync);
        CloseChatCommand = ReactiveCommand.Create(CloseChat);
        SendMessageCommand = ReactiveCommand.Create(SendMessage, 
            this.WhenAnyValue(x => x.SelectedChatFriend).Select(friend => friend != null));
        LoadFriendsAsync().ConfigureAwait(true);
    }

    private async Task LoadFriendsAsync()
    {
        var loaded = await _stateManager.GetFriendsAsync();
        Friends.Clear();
        foreach (var f in loaded)
            Friends.Add(f);
    }

    private async Task AddFriendAsync()
    {
        bool success = await _stateManager.AddFriendAsync(NewFriendUsername);
        if (success)
        {
            await LoadFriendsAsync(); // Refresh the list
        }
    }
    public void OpenChatWith(FriendModel? friend)
    {
        SelectedChatFriend = friend;
        IsChatOpen = true;
    }

    public void CloseChat()
    {
        IsChatOpen = false;
        SelectedChatFriend = null;
    }
    private void SendMessage()
    {
        if (!string.IsNullOrWhiteSpace(NewMessageText) && SelectedChatFriend != null)
        {
            var message = new MessageModel
            {
                Sender = _stateManager.SummonerInfo.GameName,
                Text = NewMessageText,
                Timestamp = DateTime.Now
            };

            SelectedChatFriend.Messages.Add(message);
            ChatMessages.Add(message);
            NewMessageText = string.Empty;

            // Simulate reply
            SimulateReply(SelectedChatFriend);
        }
    }

    private async void SimulateReply(FriendModel? friend)
    {
        await Task.Delay(1000);
        var reply = new MessageModel
        {
            Sender = friend.Username,
            Text = "Got your message!",
            Timestamp = DateTime.Now
        };
        friend.Messages.Add(reply);
    }
}