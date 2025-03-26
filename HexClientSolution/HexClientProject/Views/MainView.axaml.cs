using Avalonia.Controls;
using Avalonia.Interactivity;
using HexClientProject.ViewModels;

namespace HexClientProject.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void OnPlayButtonClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.NavigateToGameModeSelection();
            }
        }
    }
}