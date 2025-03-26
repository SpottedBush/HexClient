using Avalonia.Controls;
using Avalonia.Interactivity;
using HexClient.ViewModels;

namespace HexClient.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void OnPlayButtonClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.NavigateToGameModeSelection();
            }
        }
    }
}