using System.Collections.Generic;
using HexClientProject.Interfaces;
using HexClientProject.Models;

namespace HexClientProject.Services.Mocks;

public class SummonerMock : ISummonerService
{
    private static readonly List<SummonerInfoModel> MockSummoners =
    [
        new SummonerInfoModel
        {
            SummonerId = 015205,
            GameName = "AhriBotSum",
            TagLine = "EUW",
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
            SummonerId = 015205,
            GameName = "HerMainSum",
            TagLine = "SINJ",
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
            SummonerId = 015205,
            GameName = "HisRivenSum",
            TagLine = "CRINGE",
            SummonerLevel = 31,
            XpSinceLastLevel = 2000,
            XpUntilNextLevel = 4000,
            RankId = 1,
            DivisionId = 2,
            Lp = 47,
            Region = "EUW"
        }

    ];
    public SummonerInfoModel GetCurrentSummonerInfoModel()
    {
        return new SummonerInfoModel
        {
            Puuid = "2e63341a-e627-48ac-bb1a-9d56e2e9cc4f",
            SummonerId = 015205,
            GameName = "TouDansLTrou",
            TagLine = "CACA",
            ProfileIconId = 1,
            SummonerLevel = 31,
            XpSinceLastLevel = 2000,
            XpUntilNextLevel = 4000,
            RankId = 1,
            DivisionId = 2,
            Lp = 47,
            Region = "EUW"
        };
    }

    public SummonerInfoModel GetSummonerInfoModel(string puuid)
    {
        return new SummonerInfoModel
        {
            Puuid = puuid,
            SummonerId = 015205,
            GameName = "Benjamin",
            TagLine = "SINJ",
            ProfileIconId = 1,
            SummonerLevel = 31,
            XpSinceLastLevel = 2000,
            XpUntilNextLevel = 4000,
            RankId = 1,
            DivisionId = 2,
            Lp = 47,
            Region = "EUW"
        };
    }

    public List<SummonerInfoModel> GetSummonerInfoList(List<string> puuidList)
    {
        for (int i = 0; i < MockSummoners.Count; i++)
        {
            MockSummoners[i].Puuid = puuidList[i];
        }
        return MockSummoners;
    }
}