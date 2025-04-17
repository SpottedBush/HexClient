using System;
using Avalonia.Controls;
using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.Services.Builders;
using HexClientProject.Services.Providers;
using HexClientProject.Views;
using ReactiveUI;

namespace HexClientProject.ViewModels;

public class MainViewModel : ReactiveObject{
    private readonly StateManager _stateManager = StateManager.Instance;
    public UserControl LeftPanelContent => _stateManager.LeftPanelContent;
    public MainViewModel()
    {
        this.WhenAnyValue(x => x._stateManager.LeftPanelContent)
            .Subscribe(_ => this.RaisePropertyChanged(nameof(LeftPanelContent)));
        _stateManager.SummonerInfo = ApiProvider.SummonerService.GetCurrentSummonerInfoModel();
        _stateManager.LeftPanelContent = new GameModeSelectionView(this);
    }
}