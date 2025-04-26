using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.Services.Builders;
using HexClientProject.Services.Mocks;
using HexClientProject.StateManagers;

namespace HexClientProject.Services.Providers;

public static class ApiProvider
{
    private static bool IsOnline => StateManager.Instance.IsOnlineMode;

    public static ILobbyService LobbyService;
    public static ISummonerService SummonerService;

    public static ISocialService SocialService;
    
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
