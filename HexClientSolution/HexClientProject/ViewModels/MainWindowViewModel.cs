using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.Models;
using HexClientProject.Views;

namespace HexClientProject.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly StateManager _stateManager = StateManager.Instance;
        [ObservableProperty]
        private UserControl? _currentView;

        public MainWindowViewModel()
        {
            // Show StartView initially
            CurrentView = new StartView();
        }

        [RelayCommand]
        private void OpenLocalMainView()
        {
            _stateManager.IsOnlineMode = false;
            MainViewModel mainViewModel = new MainViewModel();
            MockingApiService.MockSetSummonerInfo();
            CurrentView = new MainView { DataContext = mainViewModel };
        }
        [RelayCommand]
        private void OpenOnlineMainView()
        {
            _stateManager.IsOnlineMode = true;
            MainViewModel mainViewModel = new MainViewModel();
            MockingApiService.MockSetSummonerInfo();
            CurrentView = new MainView { DataContext = mainViewModel };
        }
    }
}