using System;
using System.Collections.Generic;

namespace HexClientProject.Models;

public class GameMode
{
    private static readonly List<string> GameModes = ["qp", "sd", "f", "pt", "t"];
    private static readonly List<string> GameDescrs =
        ["Quick Play", "Ranked Solo/Duo", "Ranked Flex", "Practice Tool", "Tutorial"];
    private static readonly List<int> GameIconIds = [0, 1, 2, 3, 4];

    
    private string _gameModeName;
    private string _gameModeDescription;
    private int _gameModeIconId;

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

    public int GameModeIconId
    {
        get => _gameModeIconId;
        set => _gameModeIconId = value;
    }

    public GameMode(string gameModeName)
    {
        if (GameModes.Contains(gameModeName))
        {
            int index = GameModes.IndexOf(gameModeName);
            _gameModeName = gameModeName;
            _gameModeDescription = GameDescrs[index];
            _gameModeIconId = GameIconIds[index];
        }
        else
            throw new System.NotImplementedException();
    }
}