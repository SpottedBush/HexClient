using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.Views;

namespace HexClientProject.ViewModels;

public class GameModeSelectionViewModel : ObservableObject
{
    private readonly MainViewModel _mainViewModel;

    public ICommand SwitchToLobby { get; }

    public GameModeSelectionViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        SwitchToLobby = new RelayCommand(() =>
        {
            _mainViewModel.LeftPanelContent = new LobbyView(_mainViewModel); // Switch the view
        });
    }
}