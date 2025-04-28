using ReactiveUI;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using HexClientProject.Views;

namespace HexClientProject.ViewModels;

public class GameModeSelectionViewModel : ReactiveObject
{
    private readonly GlobalStateManager _globalStateManager = GlobalStateManager.Instance;

    private readonly ViewStateManager _viewStateManager = ViewStateManager.Instance;
    public ReactiveCommand<object, Unit> SwitchToLobby { get; }

    public GameModeSelectionViewModel(MainViewModel mainViewModel)
    {
        SwitchToLobby = ReactiveCommand.Create<object>(param =>
        {
            if (param is string gameModeName)
            {
                _globalStateManager.LobbyInfo = ApiProvider.LobbyService.CreateLobbyInfoModel(); // API PROVIDER
                _globalStateManager.LobbyInfo.CurrSelectedGameModeModel = new GameModeModel(gameModeName);
            }

            _viewStateManager.LeftPanelContent = new LobbyView(mainViewModel); // Switch the view
        });
    }
}