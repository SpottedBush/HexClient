using System;
using HexClientProject.ViewModels;

namespace HexClientProject.Models;

public class SummonerInfoModel
{
    
    private string _puuid = null!;
    private readonly string _gameName = null!;
    private string _tagLine = null!;
    public int RankId { get; init; }
    public int DivisionId { get; init; }
    public int Lp { get; set; }
    public string Region { get; set; } = null!;

    public string Puuid
    {
        get => _puuid;
        set => _puuid = value ?? throw new ArgumentNullException(nameof(value));
    }

    public long SummonerId { get; set; }

    public required string GameName
    {
        get => _gameName;
        init => _gameName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string GameNameTag => _gameName + "#" + _tagLine;

    public int ProfileIconId { get; init; }

    public required string TagLine
    {
        get => _tagLine;
        set => _tagLine = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int SummonerLevel { get; init; }

    public int XpSinceLastLevel { get; set; }

    public int XpUntilNextLevel { get; set; }
    
    public string RankDisplay { get; set; }

    public SummonerInfoModel()
    {
        RankDisplay = SummonerInfoViewModel.RankStrings[RankId] + " " + SummonerInfoViewModel.RankDivisions[DivisionId];
    }

}