using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using HexClientProject.Views;

namespace HexClientProject.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl _leftPanelContent;
    
    public MainViewModel()
    {
        LeftPanelContent = new GameModeSelectionView(this);
    }
    
    public void SwitchToGameModeSelection()
    {
        LeftPanelContent = new GameModeSelectionView(this);
    }
    public void SwitchToLobbyView()
    {
        LeftPanelContent = new LobbyView(this);
    }
}