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
    public static readonly IQueueService QueueService;
    public static readonly IRuneService RuneService;
    public static readonly ISocialService SocialService;
    
    static ApiProvider()
    {
        if (IsOnline)
        {
            LobbyService = new LobbyBuilder();
            SummonerService = new SummonerBuilder();
            SocialService = new SocialBuilder();
            QueueService = new QueueBuilder();
            RuneService = new RuneBuilder();
        }
        else // Mocking
        {
            SocialService = new SocialMock();
            LobbyService = new LobbyMock();
            SummonerService = new SummonerMock();
            QueueService = new QueueMock();
            RuneService = new RuneMock();
        }
    }
}
