using System.Collections.ObjectModel;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.SideBar;
using ReactiveUI;

namespace HexClientProject.StateManagers;

public class SocialStateManager : ReactiveObject
{
    private static SocialStateManager? _instance;
    // Public property to access the single instance
    public static SocialStateManager Instance
    {
        get
        {
            _instance ??= new SocialStateManager();
            return _instance;
        }
    }
    public ObservableCollection<FriendModel> Friends { get; } = [];
    public Collection<string> MutedUsernames { get; } = [];
    public ChatBoxViewModel? ChatBoxViewModel { get; set; } = null;
    public FriendsListViewModel FriendsListViewModel { get; set; } = null!;
    private void LoadFriendsAndMutedUsers()
    {
        LoadFriends();
        LoadMutedUsers();
    }
    private void LoadMutedUsers()
    {
        var mutedUserList = ApiProvider.SocialService.GetMutedUserList();
        MutedUsernames.Clear();
        foreach (var friend in mutedUserList)
        {
            MutedUsernames.Add(friend);
        }
    }

    public void LoadFriends()
    {
        var newFriends = ApiProvider.SocialService.GetFriendModelList();
        Friends.Clear();
        foreach (var friend in newFriends)
        {
            friend.ParentViewModel = FriendsListViewModel;
            Friends.Add(friend);
        }
    }
    public void AddFriend(string gameNameTag)
    {
        if (gameNameTag == string.Empty)
            return;
        bool success = ApiProvider.SocialService.AddFriend(gameNameTag);
        if (success)
            LoadFriends();
    }

    public void RemoveFriend(string gameNameTag)
    {
        bool success = ApiProvider.SocialService.RemoveFriend(gameNameTag);
        if (success)
            LoadFriends();
    }

    public void MuteUser(string gameNameTag)
    {
        bool success = ApiProvider.SocialService.MuteUser(gameNameTag);
        if (success)
            LoadMutedUsers();
    }

    public void BlockFriend(string gameNameTag)
    {
        bool success = ApiProvider.SocialService.BlockFriend(gameNameTag);
        if (success)
            LoadFriendsAndMutedUsers();
    }

    private void UnblockFriend(string gameNameTag)
    {
        bool success = ApiProvider.SocialService.UnblockFriend(gameNameTag);
        if (success)
            LoadMutedUsers();
    }
}