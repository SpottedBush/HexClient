using System.Collections.Generic;
using System.Linq;
using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.StateManagers;
using HexClientProject.Views;

namespace HexClientProject.Services.Mocks;

public class SocialMock : ISocialService
{
    private readonly SocialStateManager _socialStateManager = SocialStateManager.Instance; 
    private readonly StateManager _stateManager = StateManager.Instance; 
    private static readonly List<FriendModel> MockFriends =
    [
        new() { GameName = "AhriBot", TagLine = "EUW", Status = "Coucou les zamis", RankId = 1, DivisionId = 2 },
        new() { GameName = "HerMain", TagLine = "SINJ", Status = "J'aime beaucoup les", RankId = 1, DivisionId = 2 },
        new() { GameName = "HisRiven", TagLine = "CRINGE", Status = "RIVEN OTP", RankId = 1, DivisionId = 2 }
    ];

    private static readonly List<string> MockMutedUsers =
    ["Dushyanth#EUW", "Jidiano#TRIQUE"];
    
    public FriendModel? GetFriendModel(string puuid)
    {
        return MockFriends.FirstOrDefault(f => f.Puuid == puuid);
    }
    
    public List<FriendModel> GetFriendModelList()
    {
        return MockFriends;
    }
    
    public List<string> GetMutedUserList()
    {
        return MockMutedUsers;
    }

    public bool ViewProfile(string username)
    {
        throw new System.NotImplementedException();
    }

    public bool AddFriend(string newFriendGamerTag)
    {
        if (!newFriendGamerTag.Contains("#"))
        {
            _socialStateManager.ChatBoxViewModel.SendSystemMessage("Tag line is missing, cannot add friend.");
            return false;
        }
        var parts = newFriendGamerTag.Split('#');
        string newFriendUsername = parts[0];
        string newFriendTag = parts[1];
        FriendModel friend = new FriendModel
        {
            GameName = newFriendUsername,
            TagLine = newFriendTag,
            Status = "RIVEN OTP",
            RankId = 1,
            DivisionId = 2
        };
        if (MockFriends.Any(f => f.GameNameTag == friend.GameNameTag)) return false; // If the friend does not already exist
        MockFriends.Add(friend);
        return true;
    }

    public bool RemoveFriend(string gameNameTagToRemove)
    {
        var existing = MockFriends.FirstOrDefault(f => f.GameNameTag == gameNameTagToRemove);
        if (existing == null) return false;
        MockFriends.Remove(existing);
        return true;
    }

    public bool PostInviteToLobby(FriendModel friend)
    {
        if (_stateManager.LeftPanelContent is not LobbyView)
            return false;
        if (_stateManager.LobbyInfo.NbPlayers == _stateManager.LobbyInfo.MaxPlayersLimit)
            return false;
        _stateManager.LobbyInfo.Summoners.Add(friend);
        return true;
    }

    public bool BlockFriend(string gameNameTagToBlock)
    {
        if (MockMutedUsers.Contains(gameNameTagToBlock)) return false; // Already blocked
        MockMutedUsers.Add(gameNameTagToBlock);
        return RemoveFriend(gameNameTagToBlock);
    }

    public bool UnblockFriend(string gameNameTagToBlock)
    {
        return MockMutedUsers.Remove(gameNameTagToBlock);
    }

    public bool MuteUser(string gameNameTagToMute)
    {
        if (MockMutedUsers.Contains(gameNameTagToMute)) return false;
        MockMutedUsers.Add(gameNameTagToMute);
        return true;
    }
    public void SendMessage(MessageModel message)
    {
        _socialStateManager.ChatBoxViewModel.Messages.Add(message);
    }
    
    
}