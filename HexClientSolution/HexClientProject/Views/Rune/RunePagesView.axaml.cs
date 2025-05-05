using Avalonia.Controls;
using HexClientProject.ViewModels.Rune;

namespace HexClientProject.Views.Rune;

public partial class RunePagesView : UserControl
{
    public RunePagesView()
    {
        InitializeComponent();
        DataContext = new RunePagesViewModel();
    }
}
