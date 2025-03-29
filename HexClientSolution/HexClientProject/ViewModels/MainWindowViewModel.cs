using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.Views;

namespace HexClientProject.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserControl? _currentView;

        public MainWindowViewModel()
        {
            // Show StartView initially
            CurrentView = new StartView();
        }

        [RelayCommand]
        private void OpenMainView()
        {
            var mainView = new MainView
            {
                DataContext = new MainViewModel() // Ensure it has a ViewModel
            };
            CurrentView = mainView;
        }
    }
}