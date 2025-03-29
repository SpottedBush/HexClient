using System;
using HexClientProject.Models;

using Newtonsoft.Json;


namespace HexClienT.Models;

public static class SummonerInfo
{
    private static string _puuid;
    private static long _summonerId;
    private static string _gameName;
    private static int _profileIconId;
    private static string _tagLine;
    private static int _summonerLevel;
    private static int _xpSinceLastLevel;
    private static int _xpUntilNextLevel;

    public static async void SetSummonerInfo()
    {
        string response = await ApiService.GetSummonerInfos();
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response);

        if (jsonObject == null)
        {
            throw new Exception("Set summoner infos: Json error");
        }

        _puuid = jsonObject.puuid;
        _summonerId = jsonObject.summonerId;
        _gameName = jsonObject.gameName;
        _profileIconId = jsonObject.profileIconId;
        _tagLine = jsonObject.tagLine;
        _summonerLevel = jsonObject.summonerLevel;
        _xpSinceLastLevel = jsonObject.xpSinceLastLevel;
        _xpUntilNextLevel = jsonObject.xpUntilNextLevel;
    }
 
}