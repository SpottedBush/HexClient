using System;
using Avalonia.Controls;
using ReactiveUI;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Views;

namespace HexClientProject.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly StateManager _stateManager = StateManager.Instance;

        private UserControl? _currentView;
        public UserControl? CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public ReactiveCommand<Unit, Unit> OpenLocalMainView { get; }
        public ReactiveCommand<Unit, Unit> OpenOnlineMainView { get; }

        public MainWindowViewModel()
        {
            // Show StartView initially
            CurrentView = new StartView();

            OpenLocalMainView = ReactiveCommand.Create(OpenMainView(false));

            OpenOnlineMainView = ReactiveCommand.Create(OpenMainView(true));
        }
        Action OpenMainView(bool isOnline)
        {
            return () =>
            {
                _stateManager.IsOnlineMode = isOnline;
            var mainViewModel = new MainViewModel();
            CurrentView = new MainView { DataContext = mainViewModel };
            };
        }
    }
}