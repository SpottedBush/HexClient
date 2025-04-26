using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using HexClientProject.Models;
using HexClientProject.StateManagers;

namespace HexClientProject.ViewModels;

public partial class SummonerInfoViewModel : ObservableObject
{
    public static readonly List<string> RankStrings = ["Iron", "Bronze", "Silver", "Gold", "Platinum", "Emerald", "Diamond", "Master", "GrandMaster", "Challenger"];
    public static readonly List<string> RankDivisions = ["IV", "III", "II", "I"];
    private readonly StateManager _stateManager = StateManager.Instance;
    private SummonerInfoModel Summoner => _stateManager.SummonerInfo;
    [ObservableProperty]
    private string _summonerName;
    [ObservableProperty]
    private int _summonerIconId;
    [ObservableProperty]
    private int _summonerLevel;
    [ObservableProperty]
    private int _summonerRankId;
    [ObservableProperty]
    private int _summonerDivisionId;
    
    public SummonerInfoViewModel()
    {
        _summonerName = Summoner.GameName;
        _summonerIconId = Summoner.ProfileIconId;
        _summonerLevel = Summoner.SummonerLevel;
        _summonerRankId = Summoner.RankId;
        _summonerDivisionId = Summoner.DivisionId;
    }
}