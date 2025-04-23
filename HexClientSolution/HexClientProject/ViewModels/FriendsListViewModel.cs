using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using ReactiveUI;

namespace HexClientProject.ViewModels;

public class FriendsListViewModel : ViewModelBase
{
    private readonly StateManager _stateManager = StateManager.Instance;
    public ObservableCollection<FriendModel> Friends => _stateManager.Friends;
    public ReactiveCommand<Unit, Unit> AddFriendCommand { get; }
    public ReactiveCommand<FriendModel, Unit> RemoveFriendCommand { get; }
    public ReactiveCommand<FriendModel, Unit> BlockFriendCommand { get; }
    public ReactiveCommand<FriendModel, Unit> MuteUserCommand { get; }
    public ReactiveCommand<FriendModel, Unit> WhisperToCommand { get; }
    public ReactiveCommand<FriendModel, Unit> InviteToLobbyCommand { get; }
    private string _newFriendUsername = string.Empty;
    public string NewFriendUsername 
    {
        get => _newFriendUsername;
        set => this.RaiseAndSetIfChanged(ref _newFriendUsername, value);
    }
    private FriendModel? _selectedFriend;

    public FriendModel? SelectedFriend
    {
        get => _selectedFriend;
        set => this.RaiseAndSetIfChanged(ref _selectedFriend, value);
    }
    
    public FriendsListViewModel()
    {
        NewFriendUsername = string.Empty;
        AddFriendCommand = ReactiveCommand.Create(AddFriend);
        RemoveFriendCommand = ReactiveCommand.Create<FriendModel>(friend => RemoveFriend(friend.Username));
        BlockFriendCommand = ReactiveCommand.Create<FriendModel>(friend => BlockFriend(friend.Username));
        MuteUserCommand = ReactiveCommand.Create<FriendModel>(friend => MuteUser(friend.Username));
        WhisperToCommand = ReactiveCommand.Create<FriendModel>(friend => WhisperTo(friend.Username));
        InviteToLobbyCommand = ReactiveCommand.Create<FriendModel>(friend => ApiProvider.SocialService.PostInviteToLobby(friend));
        LoadFriends();
    }

    private void LoadFriendsAndMutedUsers()
    {
        LoadFriends();
        LoadMutedUsers();
    }
    
    private void LoadMutedUsers()
    {
        var mutedUserList = ApiProvider.SocialService.GetMutedUserList();
        _stateManager.MutedUsernames.Clear();
        foreach (var friend in mutedUserList)
        {
            _stateManager.MutedUsernames.Add(friend);
        }
    }
    
    private void LoadFriends()
    {
        var newFriends = ApiProvider.SocialService.GetFriendModelList();
        _stateManager.Friends.Clear();
        foreach (var friend in newFriends)
        {
            friend.ParentViewModel = this;
            _stateManager.Friends.Add(friend);
        }
    }

    private void AddFriend()
    {
        if (NewFriendUsername == string.Empty)
            return;
        bool success = ApiProvider.SocialService.AddFriend(NewFriendUsername);
        if (success)
            LoadFriends();
        NewFriendUsername = string.Empty; // Clear the textbox
    }
    private void RemoveFriend(string username)
    {
        bool success = ApiProvider.SocialService.RemoveFriend(username);
        if (success)
            LoadFriends();
    }

    private void MuteUser(string username)
    {
        bool success = ApiProvider.SocialService.MuteUser(username);
        if (success)
            LoadMutedUsers();
    }
    
    private void BlockFriend(string username)
    {
        bool success = ApiProvider.SocialService.BlockFriend(username);
        if (success)
            LoadFriendsAndMutedUsers();
    }

    private void UnblockFriend(string username)
    {
        bool success = ApiProvider.SocialService.UnblockFriend(username);
        if (success)
            LoadMutedUsers();
    }
    
    public void WhisperTo(string username)
    {
        _stateManager.ChatBoxViewModel.SelectedScope = ChatScope.Whisper;
        _stateManager.ChatBoxViewModel.SelectedWhisperTarget = username;
        _stateManager.ChatBoxViewModel.MessageInput = $"/mp <{username}> ";
        _stateManager.ChatBoxViewModel.ApplyFilterToWhisper(username);
    }
}