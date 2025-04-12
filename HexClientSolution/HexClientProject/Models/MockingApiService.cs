using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HexClienT.Models;

namespace HexClientProject.Models;

public static class MockingApiService
{
    static StateManager _instance = StateManager.Instance;
    public static void MockCreateLobby(string gameModeName)
        {
            Debug.Assert(_instance != null, nameof(_instance) + " != null");
            _instance.LobbyInfo = new LobbyInfoModel
            {
                LobbyName = "TouDansLTrou's Game",
                LobbyPassword = "benvoyons",
                HostName = "TouDansLTrou",
                NbPlayers = 1, 
                MaxPlayersLimit = 5,
                CanQueue = true,
                CurrSelectedGameModeModel = new GameModeModel(gameModeName),
                LeaderName = "TouDansLTrou",
                Summoners = [_instance.SummonerInfo,
                    new SummonerInfoModel
                    {
                        Puuid = "2e63341a-e627-48ac-bb1a-9d56e2e9cc4f",
                        SummonerId = 015205,
                        GameName = "TouDansLTrou(le2)", // Hey, I have a friend !
                        ProfileIconId = 1,
                        TagLine = "CACA",
                        SummonerLevel = 31,
                        XpSinceLastLevel = 2000,
                        XpUntilNextLevel = 4000,
                        RankId = 1,
                        DivisionId = 2,
                        Lp = 47,
                        Region = "EUW"
                    },
                    new SummonerInfoModel
                    {
                        Puuid = "2e63341a-e627-48ac-bb1a-9d56e2e9cc4f",
                        SummonerId = 015205,
                        GameName = "TouDansLTrou(le3)", // Another one !
                        ProfileIconId = 1,
                        TagLine = "CACA",
                        SummonerLevel = 31,
                        XpSinceLastLevel = 2000,
                        XpUntilNextLevel = 4000,
                        RankId = 1,
                        DivisionId = 2,
                        Lp = 47,
                        Region = "EUW"
                    }
                    ]
            };
        }
    
    public static void MockSetSummonerInfo()
    {
        Debug.Assert(_instance != null, nameof(_instance) + " != null");
        _instance.SummonerInfo = new SummonerInfoModel
        {
            Puuid = "2e63341a-e627-48ac-bb1a-9d56e2e9cc4f",
            SummonerId = 015205,
            GameName = "TouDansLTrou",
            ProfileIconId = 1,
            TagLine = "CACA",
            SummonerLevel = 31,
            XpSinceLastLevel = 2000,
            XpUntilNextLevel = 4000,
            RankId = 1,
            DivisionId = 2,
            Lp = 47,
            Region = "EUW"
        };
    }
    private static readonly List<FriendModel> MockFriends = new()
    {
        new FriendModel { Username = "AhriBot", Status = "Coucou les zamis", RankId = 1, DivisionId = 2 },
        new FriendModel { Username = "HerMain", Status = "J'aime beaucoup les", RankId = 1, DivisionId = 2 },
        new FriendModel { Username = "HisRiven", Status = "RIVEN OTP", RankId = 1, DivisionId = 2 }
    };

    /// Returns the current list of friends.
    public static List<FriendModel> GetFriends()
    {
        return MockFriends;
    }

    /// Adds a new friend to the list if not already present.
    public static bool AddFriend(FriendModel friend)
    {
        if (MockFriends.All(f => f.Username != friend.Username)) // If friend does not already exist
        {
            MockFriends.Add(friend);
            return true;
        }
        return false;
    }

    /// Removes a friend from the list by Username.
    public static bool RemoveFriend(string usernameToRemove)
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