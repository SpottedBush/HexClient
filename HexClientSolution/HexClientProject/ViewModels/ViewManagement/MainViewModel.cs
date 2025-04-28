using System;
using Avalonia.Controls;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using HexClientProject.Views;
using ReactiveUI;

namespace HexClientProject.ViewModels.ViewManagement;

public class MainViewModel : ReactiveObject{
    private readonly GlobalStateManager _globalStateManager = GlobalStateManager.Instance;
    private readonly ViewStateManager _viewStateManager = ViewStateManager.Instance;
    public UserControl LeftPanelContent => _viewStateManager.LeftPanelContent;
    public MainViewModel()
    {
        this.WhenAnyValue(x => x._viewStateManager.LeftPanelContent)
            .Subscribe(_ => this.RaisePropertyChanged(nameof(LeftPanelContent)));
        _globalStateManager.SummonerInfo = ApiProvider.SummonerService.GetCurrentSummonerInfoModel();
        _viewStateManager.LeftPanelContent = new GameModeSelectionView(this);
    }
}