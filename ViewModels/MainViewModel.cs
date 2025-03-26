using ReactiveUI;

namespace HexClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public MainViewModel()
        {
            _currentViewModel = this;
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }

        public void NavigateToGameModeSelection()
        {
            CurrentViewModel = new GameModeSelectionViewModel();
        }
    }
}