using CommunityToolkit.Mvvm.ComponentModel;
using HexClientProject.ViewModels;
using HexClientProject.Views;

public class MainViewModel : ObservableObject
{
    private object _leftPanelView;
    public object LeftPanelView
    {
        get => _leftPanelView;
        set => SetProperty(ref _leftPanelView, value);
    }

    public MainViewModel()
    {
        var gameModeVM = new HexClientProject.ViewModels.GameModeSelectionViewModel();
        gameModeVM.RequestLobbyView += SwitchToLobby;
        LeftPanelView = new GameModeSelectionView { DataContext = gameModeVM };
    }

    public void SwitchToLobby()
    {
        LeftPanelView = new LobbyView { DataContext = new LobbyViewModel() };
    }
}