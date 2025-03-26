using System;

namespace HexClientProject.Models;

public static class LobbyInfo
{
    private static string _lobbyName;
    private static string _lobbyPassword;
    private static string _hostName;
    private static int _nbPlayers;
    private static int _maxPlayersLimit;
    private static bool _canQueue;
    private static GameMode _currSelectedGameMode;
    
    public static string LobbyName
    {
        get => _lobbyName;
        set => _lobbyName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static string LobbyPassword
    {
        get => _lobbyPassword;
        set => _lobbyPassword = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static string HostName
    {
        get => _hostName;
        set => _hostName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static int NbPlayers
    {
        get => _nbPlayers;
        set => _nbPlayers = value;
    }

    public static int MaxPlayersLimit
    {
        get => _maxPlayersLimit;
        set => _maxPlayersLimit = value;
    }

    public static bool CanQueue
    {
        get => _canQueue;
        set => _canQueue = value;
    }

    public static GameMode CurrSelectedGameMode
    {
        get => _currSelectedGameMode;
        set => _currSelectedGameMode = value ?? throw new ArgumentNullException(nameof(value));
    }
}