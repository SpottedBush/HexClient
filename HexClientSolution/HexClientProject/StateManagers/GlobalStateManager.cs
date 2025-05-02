using HexClientProject.Models;
using ReactiveUI;

namespace HexClientProject.StateManagers;

public class GlobalStateManager : ReactiveObject
{
    private static GlobalStateManager? _instance;

    // Public property to access the single instance
    public static GlobalStateManager Instance
    {
        get
        {
            _instance ??= new GlobalStateManager();
            return _instance;
        }
    }
    public bool IsOnlineMode;
    public bool IsInDraft = false;
    public LobbyInfoModel LobbyInfo { get; set; } = null!;
    public SummonerInfoModel SummonerInfo { get; set; } = null!;
}