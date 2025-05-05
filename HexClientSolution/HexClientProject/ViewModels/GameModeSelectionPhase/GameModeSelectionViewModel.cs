using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using HexClientProject.ViewModels.ViewManagement;
using HexClientProject.Views;
using ReactiveUI;
using LobbyView = HexClientProject.Views.LobbyPhase.LobbyView;

namespace HexClientProject.ViewModels.GameModeSelectionPhase;

public class GameModeSelectionViewModel : ReactiveObject
{
    private readonly GlobalStateManager _globalStateManager = GlobalStateManager.Instance;

    private readonly ViewStateManager _viewStateManager = ViewStateManager.Instance;
    public ReactiveCommand<object, Unit> SwitchToLobby { get; }

    public GameModeSelectionViewModel()
    {
        SwitchToLobby = ReactiveCommand.Create<object>(param =>
        {
            if (param is string gameModeName)
            {
                _globalStateManager.LobbyInfo = ApiProvider.LobbyService.CreateLobbyInfoModel(); // API PROVIDER
                _globalStateManager.LobbyInfo.CurrSelectedGameModeModel = new GameModeModel(gameModeName);
            }

            _viewStateManager.LeftPanelContent = new LobbyView(); // Switch the view
        });
    }
}