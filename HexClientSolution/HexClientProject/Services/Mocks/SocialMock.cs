using System.Collections.Generic;
using System.Linq;
using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.Views;

namespace HexClientProject.Services.Mocks;

public class SocialMock : ISocialService
{
    private readonly StateManager _stateManager = StateManager.Instance; 
    private static readonly List<FriendModel> MockFriends =
    [
        new() { Username = "AhriBot", Status = "Coucou les zamis", RankId = 1, DivisionId = 2 },
        new() { Username = "HerMain", Status = "J'aime beaucoup les", RankId = 1, DivisionId = 2 },
        new() { Username = "HisRiven", Status = "RIVEN OTP", RankId = 1, DivisionId = 2 }
    ];

    private static readonly List<string> MockMutedUsers =
    ["Dushyanth", "Jidiano"];
    
    public FriendModel? GetFriendModel(string puuid)
    {
        return MockFriends.FirstOrDefault(f => f.Username == puuid);
    }
    
    public List<FriendModel> GetFriendModelList()
    {
        return MockFriends;
    }
    
    public List<string> GetMutedUserList()
    {
        return MockMutedUsers;
    }
    
    public bool AddFriend(string newFriendUsername)
    {
        FriendModel friend = new FriendModel
        {
            Username = newFriendUsername,
            Status = "RIVEN OTP",
            RankId = 1,
            DivisionId = 2
        };
        if (MockFriends.Any(f => f.Username == friend.Username)) return false; // If the friend does not already exist
        MockFriends.Add(friend);
        return true;
    }

    public bool RemoveFriend(string usernameToRemove)
    {
        var existing = MockFriends.FirstOrDefault(f => f.Username == usernameToRemove);
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

    public bool BlockFriend(string usernameToBlock)
    {
        if (MockMutedUsers.Contains(usernameToBlock)) return false; // Already blocked
        MockMutedUsers.Add(usernameToBlock);
        return RemoveFriend(usernameToBlock);
    }

    public bool UnblockFriend(string usernameToBlock)
    {
        return MockMutedUsers.Remove(usernameToBlock);
    }

    public bool MuteUser(string usernameToMute)
    {
        if (MockMutedUsers.Contains(usernameToMute)) return false;
        MockMutedUsers.Add(usernameToMute);
        return true;
    }
    public void SendMessage(MessageModel message)
    {
        throw new System.NotImplementedException();
    }
    
    
}