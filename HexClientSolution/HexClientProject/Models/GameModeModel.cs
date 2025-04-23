using System;
using System.Collections.Generic;

namespace HexClientProject.Models;

public class GameModeModel
{
    private static readonly List<string> GameModes = ["Qp", "Sd", "F", "Pt", "T"];
    private static readonly List<string> GameDescriptions =
        ["Quick Play", "Ranked Solo/Duo", "Ranked Flex", "Practice Tool", "Tutorial"];
    private static readonly List<int> GameIconIds = [0, 1, 2, 3, 4];
    private static readonly List<string> GameModeIds = ["490", "420", "440", "0", "2000"];


    private string _gameModeName;
    private string _gameModeDescription;

    public string GameModeName
    {
        get => _gameModeName;
        set => _gameModeName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string GameModeDescription
    {
        get => _gameModeDescription;
        set => _gameModeDescription = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int GameModeIconId { get; set; }

    public string GameModeId { get; set; }

    public GameModeModel(string gameModeName)
    {
        if (GameModes.Contains(gameModeName))
        {
            int index = GameModes.IndexOf(gameModeName);
            _gameModeName = gameModeName;
            _gameModeDescription = GameDescriptions[index];
            GameModeIconId = GameIconIds[index];
            GameModeId = GameModeIds[index];
        }
        else
            throw new NotImplementedException();
    }

    public static string GetGameModeFromGameId(string gameId)
    {
        if (GameModeIds.Contains(gameId))
        {
            return GameModes[GameModeIds.IndexOf(gameId)];
        }
        else
        {
            throw new Exception("Err: wrong game id: " + gameId);
        }
    }

    public static string GetGameIdFromGameMode(string gameMode)
    {
        if (GameModes.Contains(gameMode))
        {
            return GameModeIds[GameModes.IndexOf(gameMode)];
        }
        else
        {
            throw new Exception("Err: wrong game mode name: " + gameMode);
        }
    }
}