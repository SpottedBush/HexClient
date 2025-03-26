using Avalonia.Controls;
using Avalonia.Interactivity;

namespace HexClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnPlayButtonClick(object? sender, RoutedEventArgs e)
        {
            Content = new Views.GameModeSelectionView();
        }
    }
}