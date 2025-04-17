using System;
using System.Collections.Generic;
using HexClienT.Models;
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
    } 
}