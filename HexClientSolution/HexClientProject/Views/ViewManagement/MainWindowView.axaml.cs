using Avalonia.Controls;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.ViewManagement;

namespace HexClientProject.Views.ViewManagement;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        var screen = Screens.Primary;
        Width = screen!.Bounds.Width;
        Height = screen.Bounds.Height;
    }
}