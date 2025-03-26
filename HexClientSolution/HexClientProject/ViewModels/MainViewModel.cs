using ReactiveUI;
using System;
using System.Reactive;

namespace HexClientProject.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ReactiveCommand<Unit, Unit> NavigateToGameModeCommand { get; }

        public MainViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            NavigateToGameModeCommand = ReactiveCommand.Create(() => _mainWindowViewModel.NavigateToGameModeSelection());
        }
    }
}