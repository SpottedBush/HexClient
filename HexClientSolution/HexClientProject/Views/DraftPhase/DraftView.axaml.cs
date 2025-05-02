using Avalonia.Controls;
using HexClientProject.ViewModels.DraftPhase;

namespace HexClientProject.Views.DraftPhase;

public partial class DraftView : UserControl
{
    public DraftView()
    {
        InitializeComponent();
        DataContext = new DraftViewModel();
    }
}