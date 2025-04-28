using HexClientProject.Interfaces;
using HexClientProject.Services.Builders;
using HexClientProject.Services.Mocks;
using HexClientProject.StateManagers;

namespace HexClientProject.Services.Providers;

public static class ApiProvider
{
    private static bool IsOnline => GlobalStateManager.Instance.IsOnlineMode;

    public static readonly ILobbyService LobbyService;
    public static readonly ISummonerService SummonerService;

    public static readonly ISocialService SocialService;
    
    static ApiProvider()
    {
        if (IsOnline)
        {
            LobbyService = new LobbyBuilder();
            SummonerService = new SummonerBuilder();
            SocialService = new SocialBuilder();
        }
        else // Mocking
        {
            SocialService = new SocialMock();
            LobbyService = new LobbyMock();
            SummonerService = new SummonerMock();
        }
    }
}
