using Avalonia.Controls;
using HexClientProject.ViewModels.GameModeSelectionPhase;

namespace HexClientProject.Views.GameModeSelectionPhase
{
    public partial class GameModeSelectionView : UserControl
    {
        public GameModeSelectionView()
        {
            InitializeComponent();
            DataContext = new GameModeSelectionViewModel();
        }
    }
}