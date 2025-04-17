using HexClienT.Models;
using HexClientProject.Interfaces;
using HexClientProject.Models;

namespace HexClientProject.Services.Mocks;

public class LobbyMock : ILobbyService
{
    public LobbyInfoModel CreateLobbyInfoModel()
    {
        return new LobbyInfoModel
        {
            LobbyName = "TouDansLTrou's Game",
            LobbyPassword = "benvoyons",
            HostName = "TouDansLTrou",
            NbPlayers = 1, 
            MaxPlayersLimit = 5,
            CanQueue = true,
            // CurrSelectedGameModeModel = new GameModeModel("RANKED_SOLO_5x5"),
            LeaderName = "TouDansLTrou",
            Summoners = [StateManager.Instance.SummonerInfo,
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
}