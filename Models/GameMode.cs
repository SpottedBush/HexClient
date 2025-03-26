using System.Collections.Generic;

namespace HexClient.Models;

public class GameMode
{
    private static readonly List<string> GameModes = ["qp", "sd", "f", "pt", "t"];
    private static readonly List<string> GameDescrs =
        ["Quick Play", "Ranked Solo/Duo", "Ranked Flex", "Practice Tool", "Tutorial"];
    private static readonly List<int> GameIconIds = [0, 1, 2, 3, 4];
    // private static List<string> ;

    
    private string _gameModeName;
    private string _gameModeDescription;
    private int _gameModeIconId;

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