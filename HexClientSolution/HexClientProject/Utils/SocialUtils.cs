

using HexClientProject.Models;
using HexClientProject.Services.Providers;

namespace HexClientProject.Utils;

public static class SocialUtils
{
    private static readonly StateManager StateManager = StateManager.Instance;
    private static void LoadFriendsAndMutedUsers()
    {
        LoadFriends();
        LoadMutedUsers();
    }

    private static void LoadMutedUsers()
    {
        var mutedUserList = ApiProvider.SocialService.GetMutedUserList();
        StateManager.MutedUsernames.Clear();
        foreach (var friend in mutedUserList)
        {
            StateManager.MutedUsernames.Add(friend);
        }
    }

    public static void LoadFriends()
    {
        var newFriends = ApiProvider.SocialService.GetFriendModelList();
        StateManager.Friends.Clear();
        foreach (var friend in newFriends)
        {
            friend.ParentViewModel = StateManager.FriendsListViewModel;
            StateManager.Friends.Add(friend);
        }
    }

    public static void ViewProfile(string username)
    {
        if (username == string.Empty)
            return;
        ApiProvider.SocialService.ViewProfile(username);
    }

    public static void AddFriend(string username)
    {
        if (username == string.Empty)
            return;
        bool success = ApiProvider.SocialService.AddFriend(username);
        if (success)
            LoadFriends();
    }

    public static void RemoveFriend(string username)
    {
        bool success = ApiProvider.SocialService.RemoveFriend(username);
        if (success)
            LoadFriends();
    }

    public static void MuteUser(string username)
    {
        bool success = ApiProvider.SocialService.MuteUser(username);
        if (success)
            LoadMutedUsers();
    }

    public static void BlockFriend(string username)
    {
        bool success = ApiProvider.SocialService.BlockFriend(username);
        if (success)
            LoadFriendsAndMutedUsers();
    }

    private static void UnblockFriend(string username)
    {
        bool success = ApiProvider.SocialService.UnblockFriend(username);
        if (success)
            LoadMutedUsers();
    }
    
    public static void WhisperTo(string username, bool changeFilteringScope = true)
    {
        StateManager.ChatBoxViewModel.SelectedWhisperTarget = username;
        StateManager.ChatBoxViewModel.MessageInput = $"/mp <{username}> ";
        if (!changeFilteringScope) return;
        StateManager.ChatBoxViewModel.SelectedScope = ChatScope.Whisper;
        StateManager.ChatBoxViewModel.ApplyFilterToWhisper(username);
    }
}