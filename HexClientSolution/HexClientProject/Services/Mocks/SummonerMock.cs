using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using HexClienT.Models;
using HexClientProject.Interfaces;
using HexClientProject.Models;

namespace HexClientProject.Services.Mocks;

public class SummonerMock : ISummonerService
{
    private static readonly List<SummonerInfoModel> MockSummoners = new()
    {
        new SummonerInfoModel
        {
            SummonerId = 015205,
            GameName = "AhriBotSum",
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
            SummonerLevel = 31,
            XpSinceLastLevel = 2000,
            XpUntilNextLevel = 4000,
            RankId = 1,
            DivisionId = 2,
            Lp = 47,
            Region = "EUW"
        },new SummonerInfoModel
        {
            SummonerId = 015205,
            GameName = "HisRivenSum",
            SummonerLevel = 31,
            XpSinceLastLevel = 2000,
            XpUntilNextLevel = 4000,
            RankId = 1,
            DivisionId = 2,
            Lp = 47,
            Region = "EUW"
        },
    };
    public SummonerInfoModel GetCurrentSummonerInfoModel()
    {
        return new SummonerInfoModel
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

    public SummonerInfoModel GetSummonerInfoModel(string puuid)
    {
        return new SummonerInfoModel
        {
            Puuid = puuid,
            SummonerId = 015205,
            GameName = "Benjamin",
            ProfileIconId = 1,
            TagLine = "SINJ",
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