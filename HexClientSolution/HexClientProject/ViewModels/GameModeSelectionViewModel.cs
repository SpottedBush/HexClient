using ReactiveUI;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.Views;

namespace HexClientProject.ViewModels;

public class GameModeSelectionViewModel : ReactiveObject
{
    private readonly StateManager _stateManager = StateManager.Instance;
    public ReactiveCommand<object, Unit> SwitchToLobby { get; }

    public GameModeSelectionViewModel(MainViewModel mainViewModel)
    {
        SwitchToLobby = ReactiveCommand.Create<object>(param =>
        {
            if (param is string gameModeName)
            {
                _stateManager.LobbyInfo = ApiProvider.LobbyService.CreateLobbyInfoModel(); // API PROVIDER
                _stateManager.LobbyInfo.CurrSelectedGameModeModel = new GameModeModel(gameModeName);
            }

            _stateManager.LeftPanelContent = new LobbyView(mainViewModel); // Switch the view
        });
    }
}