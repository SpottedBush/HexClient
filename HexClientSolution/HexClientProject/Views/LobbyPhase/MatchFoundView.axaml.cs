using Avalonia.Controls;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.LobbyPhase;

namespace HexClientProject.Views
{
    public partial class MatchFoundView : UserControl
    {
        public MatchFoundView(MatchFoundViewModel matchFoundViewModel)
        {
            InitializeComponent();
            DataContext = matchFoundViewModel;
        }
    }
}