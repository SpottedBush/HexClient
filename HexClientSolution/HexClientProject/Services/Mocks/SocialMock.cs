using System.Collections.Generic;
using System.Linq;
using HexClientProject.Interfaces;
using HexClientProject.Models;

namespace HexClientProject.Services.Mocks;

public class SocialMock : ISocialService
{
    private static readonly List<FriendModel> MockFriends = new()
    {
        new FriendModel { Username = "AhriBot", Status = "Coucou les zamis", RankId = 1, DivisionId = 2 },
        new FriendModel { Username = "HerMain", Status = "J'aime beaucoup les", RankId = 1, DivisionId = 2 },
        new FriendModel { Username = "HisRiven", Status = "RIVEN OTP", RankId = 1, DivisionId = 2 }
    };
    
    public FriendModel? GetFriendModel(string puuid)
    {
        return MockFriends.FirstOrDefault(f => f.Username == puuid);
    }
    
    public List<FriendModel> GetFriendModelList()
    {
        return MockFriends;
    }

    public bool AddFriend(FriendModel friend)
    {
        if (MockFriends.All(f => f.Username != friend.Username)) // If friend does not already exist
        {
            MockFriends.Add(friend);
            return true;
        }
        return false;
    }

    public bool RemoveFriend(string usernameToRemove)
    {
        var existing = MockFriends.FirstOrDefault(f => f.Username == usernameToRemove);
        if (existing != null)
        {
            MockFriends.Remove(existing);
            return true;
        }
        return false;
    }
}