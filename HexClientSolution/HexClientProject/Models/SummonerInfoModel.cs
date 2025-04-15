using System;
using System.Collections.Generic;
using HexClientProject.ApiServices;
using Newtonsoft.Json;


namespace HexClienT.Models;

public class SummonerInfoModel
{
    
    private string _puuid;
    private long _summonerId;
    private string _gameName;
    private int _profileIconId;
    private string _tagLine;
    private int _summonerLevel;
    private int _xpSinceLastLevel;
    private int _xpUntilNextLevel;
    private int _rankId;
    private int _divisionId;
    private int _lp;
    private string _region;
    public int RankId { get; set; }
    public int DivisionId { get; set; }
    public int Lp { get; set; }
    public string Region { get; set; }
    public string Puuid
    {
        get => _puuid;
        set => _puuid = value ?? throw new ArgumentNullException(nameof(value));
    }

    public long SummonerId
    {
        get => _summonerId;
        set => _summonerId = value;
    }

    public string GameName
    {
        get => _gameName;
        set => _gameName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int ProfileIconId
    {
        get => _profileIconId;
        set => _profileIconId = value;
    }

    public string TagLine
    {
        get => _tagLine;
        set => _tagLine = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int SummonerLevel
    {
        get => _summonerLevel;
        set => _summonerLevel = value;
    }

    public int XpSinceLastLevel
    {
        get => _xpSinceLastLevel;
        set => _xpSinceLastLevel = value;
    }

    public int XpUntilNextLevel
    {
        get => _xpUntilNextLevel;
        set => _xpUntilNextLevel = value;
    }
    public async void SetSummonerInfo()
    {
    }
}