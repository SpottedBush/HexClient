using System;
using Avalonia.Controls;
using HexClientProject.Models;
using HexClientProject.Views;
using ReactiveUI;

namespace HexClientProject.ViewModels;

public class MainViewModel : ReactiveObject{
    private readonly StateManager _stateManager;

    public UserControl LeftPanelContent => _stateManager.LeftPanelContent;
    
    public MainViewModel()
    {
        _stateManager = StateManager.Instance;
        this.WhenAnyValue(x => x._stateManager.LeftPanelContent)
            .Subscribe(_ => this.RaisePropertyChanged(nameof(LeftPanelContent)));
        
        SwitchToGameModeSelection();
    }
    
    public void SwitchToGameModeSelection()
    {
        _stateManager.LeftPanelContent = new GameModeSelectionView(this);
    }
    public void SwitchToLobbyView()
    {
        _stateManager.LeftPanelContent = new LobbyView(this);
    }
}