using System;
using System.Reactive;
using Avalonia.Controls;
using HexClientProject.StateManagers;
using HexClientProject.Views;
using HexClientProject.Views.DraftPhase;
using ReactiveUI;

namespace HexClientProject.ViewModels.ViewManagement;

public class MainWindowViewModel : ReactiveObject
{
    private readonly ViewStateManager _viewStateManager = ViewStateManager.Instance;

    public UserControl CurrentView => _viewStateManager.CurrView;

    public ReactiveCommand<Unit, Unit> OpenLocalMainView { get; }
    public ReactiveCommand<Unit, Unit> OpenOnlineMainView { get; }

    public MainWindowViewModel()
    {
        this.WhenAnyValue(x => x._viewStateManager.CurrView)
            .Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentView)));
        // Show StartView initially
        _viewStateManager.CurrView = new StartView();
        // _viewStateManager.CurrView = new DraftView(); // TO REMOVE DEV PURPOSE ONLY
        // GlobalStateManager.Instance.IsOnlineMode = false; // TO REMOVE DEV PURPOSE ONLY
        OpenLocalMainView = ReactiveCommand.Create(() => OpenMainView(false)());
        OpenOnlineMainView = ReactiveCommand.Create(() => OpenMainView(true)());

    }
    Action OpenMainView(bool isOnline)
    {
        return () =>
        {
            GlobalStateManager.Instance.IsOnlineMode = isOnline;
            var mainViewModel = new MainViewModel();
            _viewStateManager.CurrView = new MainView { DataContext = mainViewModel };
        };
    }
}