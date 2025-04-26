using System;
using Avalonia.Controls;
using ReactiveUI;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.StateManagers;
using HexClientProject.Views;

namespace HexClientProject.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly StateManager _stateManager = StateManager.Instance;

        public UserControl CurrentView => _stateManager.CurrView;

        public ReactiveCommand<Unit, Unit> OpenLocalMainView { get; }
        public ReactiveCommand<Unit, Unit> OpenOnlineMainView { get; }

        public MainWindowViewModel()
        {
            this.WhenAnyValue(x => x._stateManager.CurrView)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentView)));
            // Show StartView initially
            _stateManager.CurrView = new StartView();

            OpenLocalMainView = ReactiveCommand.Create(() => OpenMainView(false)());
            OpenOnlineMainView = ReactiveCommand.Create(() => OpenMainView(true)());

        }
        Action OpenMainView(bool isOnline)
        {
            return () =>
            {
                _stateManager.IsOnlineMode = isOnline;
                var mainViewModel = new MainViewModel();
                _stateManager.CurrView = new MainView { DataContext = mainViewModel };
            };
        }
    }
}