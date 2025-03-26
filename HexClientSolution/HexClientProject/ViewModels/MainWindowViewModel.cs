using ReactiveUI;

namespace HexClientProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public MainWindowViewModel()
        {
            CurrentView = new MainViewModel(this); // Set default view
        }

        public void NavigateToGameModeSelection()
        {
            CurrentView = new GameModeSelectionViewModel();
        }
    }
}