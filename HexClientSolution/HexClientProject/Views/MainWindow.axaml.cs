using Avalonia.Controls;
using Avalonia.Interactivity;

namespace HexClientProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnPlayButtonClick(object? sender, RoutedEventArgs e)
        {
            Content = new MainView();
        }
    }
}