using System;
using System.Collections.Generic;
using HexClienT.Models;
using HexClientProject.ApiServices;
using Newtonsoft.Json;

namespace HexClientProject.Models;

public class LobbyInfoModel()
{
    private string? _lobbyName;
    private string? _lobbyPassword;
    private string? _leaderName;
    private int _nbPlayers;
    private int _maxPlayersLimit;
    private bool _canQueue;
    private GameModeModel? _currSelectedGameModeModel;
    private List<SummonerInfoModel>? _summoners;

    public string? LeaderName
    {
        get => _leaderName;
        set => _leaderName = value;
    }

    public List<SummonerInfoModel>? Summoners
    {
        get => _summoners;
        set => _summoners = value;
    }

    public string LobbyName
    {
        get => _lobbyName ?? throw new InvalidOperationException();
        set => _lobbyName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LobbyPassword
    {
        get => _lobbyPassword ?? throw new InvalidOperationException();
        set => _lobbyPassword = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string HostName
    {
        get => _leaderName ?? throw new InvalidOperationException();
        set => _leaderName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int NbPlayers
    {
        get => _nbPlayers;
        set => _nbPlayers = value;
    }

    public int MaxPlayersLimit
    {
        get => _maxPlayersLimit;
        set => _maxPlayersLimit = value;
    }

    public bool CanQueue
    {
        get => _canQueue;
        set => _canQueue = value;
    }

    public GameModeModel CurrSelectedGameModeModel
    {
        get => _currSelectedGameModeModel ?? throw new InvalidOperationException();
        set => _currSelectedGameModeModel = value ?? throw new ArgumentNullException(nameof(value));
    }

    public async void SetLobbyInfo()
    {
        string response = await ApiServices.ApiServices.GetLobbyInfos();
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

        if (jsonObject == null)
        {
            throw new Exception("Set lobby infos: Json error");
        }

        _lobbyName = jsonObject.gameConfig.customLobbyName;
        _lobbyPassword = "IDKLOL"; //TODO Louis Change this password I beg you...
        
        foreach (var m in jsonObject.members) 
        {
            if (m.isLeader)
            {
                _leaderName = (m.summonerId).ToString();
                break;
            }

        }

        _nbPlayers = jsonObject["members"].Count();
        _maxPlayersLimit = jsonObject.gameConfig.maxLobbySize;
        _canQueue = jsonObject.canStartActivity;
        _currSelectedGameModeModel = new GameModeModel(GameModeModel.GetGameModeFromGameId(jsonObject.gameConfig.queueId));
        _summoners = new List<SummonerInfoModel>(jsonObject.members); //TODO Louis -> Parse this logic
    } 
}