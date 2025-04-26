

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

    public static void ViewProfile(string gameNameTag)
    {
        if (gameNameTag == string.Empty)
            return;
        ApiProvider.SocialService.ViewProfile(gameNameTag);
    }

    public static void AddFriend(string gameNameTag)
    {
        if (gameNameTag == string.Empty)
            return;
        bool success = ApiProvider.SocialService.AddFriend(gameNameTag);
        if (success)
            LoadFriends();
    }

    public static void RemoveFriend(string gameNameTag)
    {
        bool success = ApiProvider.SocialService.RemoveFriend(gameNameTag);
        if (success)
            LoadFriends();
    }

    public static void MuteUser(string gameNameTag)
    {
        bool success = ApiProvider.SocialService.MuteUser(gameNameTag);
        if (success)
            LoadMutedUsers();
    }

    public static void BlockFriend(string gameNameTag)
    {
        bool success = ApiProvider.SocialService.BlockFriend(gameNameTag);
        if (success)
            LoadFriendsAndMutedUsers();
    }

    private static void UnblockFriend(string gameNameTag)
    {
        bool success = ApiProvider.SocialService.UnblockFriend(gameNameTag);
        if (success)
            LoadMutedUsers();
    }
    
    public static void WhisperTo(string gameNameTag, bool changeFilteringScope = true)
    {
        StateManager.ChatBoxViewModel.SelectedWhisperTarget = gameNameTag;
        StateManager.ChatBoxViewModel.MessageInput = $"/mp <{gameNameTag}> ";
        if (!changeFilteringScope) return;
        StateManager.ChatBoxViewModel.SelectedScope = ChatScope.Whisper;
        StateManager.ChatBoxViewModel.ApplyFilterToWhisper(gameNameTag);
    }
}