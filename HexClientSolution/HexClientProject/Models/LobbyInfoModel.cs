using System;
using System.Collections.Generic;

namespace HexClientProject.Models;

public class LobbyInfoModel
{
    private string? _lobbyName;
    private string? _lobbyPassword;
    private string? _leaderName;
    private GameModeModel? _currSelectedGameModeModel;

    public string? LeaderName
    {
        get => _leaderName;
        set => _leaderName = value;
    }

    public List<SummonerInfoModel> Summoners { get; set; } = new();

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

    public int NbPlayers { get; set; }

    public int MaxPlayersLimit { get; set; }

    public GameModeModel CurrSelectedGameModeModel
    {
        get => _currSelectedGameModeModel ?? throw new InvalidOperationException();
        set => _currSelectedGameModeModel = value ?? throw new ArgumentNullException(nameof(value));
    }
}