using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace HexClienT.Models;

public class SummonerInfoModel
{
    
    private string _puuid;
    private string _gameName;
    private string _tagLine;
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

    public long SummonerId { get; set; }

    public string GameName
    {
        get => _gameName;
        set => _gameName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int ProfileIconId { get; set; }

    public string TagLine
    {
        get => _tagLine;
        set => _tagLine = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int SummonerLevel { get; set; }

    public int XpSinceLastLevel { get; set; }

    public int XpUntilNextLevel { get; set; }
}