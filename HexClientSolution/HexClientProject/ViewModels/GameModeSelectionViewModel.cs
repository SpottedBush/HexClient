using System;
using System.Windows.Input;
using ReactiveUI;
using System.Reactive;
using HexClientProject.Models;
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
                MockingApiService.MockCreateLobby(gameModeName);
            }

            _stateManager.LeftPanelContent = new LobbyView(mainViewModel); // Switch the view
        });
    }
}