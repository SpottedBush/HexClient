using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using HexClientProject.Utils;
using ReactiveUI;

namespace HexClientProject.ViewModels;

public class FriendsListViewModel : ViewModelBase
{
    private readonly StateManager _stateManager = StateManager.Instance;
    private readonly SocialStateManager _socialStateManager = SocialStateManager.Instance;
    public ObservableCollection<FriendModel> Friends => _socialStateManager.Friends;
    public ReactiveCommand<FriendModel, Unit> ViewProfileCommand { get; }
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
        _socialStateManager.FriendsListViewModel = this;
        NewFriendUsername = string.Empty;
        AddFriendCommand = ReactiveCommand.Create(AddFriend);
        ViewProfileCommand = ReactiveCommand.Create<FriendModel>(friend => SocialUtils.ViewProfile(friend.GameNameTag));
        RemoveFriendCommand = ReactiveCommand.Create<FriendModel>(friend => SocialUtils.RemoveFriend(friend.GameNameTag));
        BlockFriendCommand = ReactiveCommand.Create<FriendModel>(friend => SocialUtils.BlockFriend(friend.GameNameTag));
        MuteUserCommand = ReactiveCommand.Create<FriendModel>(friend => SocialUtils.MuteUser(friend.GameNameTag));
        WhisperToCommand = ReactiveCommand.Create<FriendModel>(friend => SocialUtils.WhisperTo(friend.GameNameTag));
        InviteToLobbyCommand = ReactiveCommand.Create<FriendModel>(friend => ApiProvider.SocialService.PostInviteToLobby(friend));
        SocialUtils.LoadFriends();
    }

    private void AddFriend()
    {
        SocialUtils.AddFriend(NewFriendUsername);
        NewFriendUsername = string.Empty; // Clear the textbox
    }
}