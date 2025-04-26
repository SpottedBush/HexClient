using System;

namespace HexClientProject.Models;

public class SummonerInfoModel
{
    
    private string _puuid = null!;
    private string _gameName = null!;
    private string _tagLine = null!;
    public int RankId { get; set; }
    public int DivisionId { get; set; }
    public int Lp { get; set; }
    public string Region { get; set; } = null!;

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

    public string GameNameTag => _gameName + _tagLine;

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