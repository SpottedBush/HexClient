using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.Models;
using HexClientProject.Views;

namespace HexClientProject.ViewModels;

public class GameModeSelectionViewModel : ObservableObject
{
    private readonly StateManager _stateManager = StateManager.Instance;
    public ICommand SwitchToLobby { get; }

    public GameModeSelectionViewModel(MainViewModel mainViewModel)
    {
        SwitchToLobby = new RelayCommand<object>((param) =>
        {

            if (param is string gameModeName)
            {
                MockingApiService.MockCreateLobby(gameModeName);
            }
            _stateManager.LeftPanelContent = new LobbyView(mainViewModel); // Switch the view
        });
    }
}