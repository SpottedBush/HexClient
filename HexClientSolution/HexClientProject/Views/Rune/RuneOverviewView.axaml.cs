using Avalonia.Controls;
using HexClientProject.ViewModels.RuneSystem;

namespace HexClientProject.Views.Rune;

public partial class RuneOverviewView : UserControl
{
    public RuneOverviewView()
    {
        InitializeComponent();
        DataContext = new RuneOverviewViewModel();
    }
}