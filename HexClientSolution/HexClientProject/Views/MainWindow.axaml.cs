using Avalonia.Controls;
using HexClientProject.ViewModels;

namespace HexClientProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}