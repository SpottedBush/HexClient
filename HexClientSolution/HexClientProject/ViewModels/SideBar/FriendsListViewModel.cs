using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using HexClientProject.Utils;
using ReactiveUI;

namespace HexClientProject.ViewModels.SideBar;

public class FriendsListViewModel : ReactiveObject
{
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
        RemoveFriendCommand = ReactiveCommand.Create<FriendModel>(friend => _socialStateManager.RemoveFriend(friend.GameNameTag));
        BlockFriendCommand = ReactiveCommand.Create<FriendModel>(friend => _socialStateManager.BlockFriend(friend.GameNameTag));
        MuteUserCommand = ReactiveCommand.Create<FriendModel>(friend => _socialStateManager.MuteUser(friend.GameNameTag));
        WhisperToCommand = ReactiveCommand.Create<FriendModel>(friend => _socialStateManager.ChatBoxViewModel.WhisperTo(friend.GameNameTag));
        InviteToLobbyCommand = ReactiveCommand.Create<FriendModel>(friend => ApiProvider.SocialService.PostInviteToLobby(friend));
        _socialStateManager.LoadFriends();
    }

    private void AddFriend()
    {
        _socialStateManager.AddFriend(NewFriendUsername);
        NewFriendUsername = string.Empty; // Clear the textbox
    }
}