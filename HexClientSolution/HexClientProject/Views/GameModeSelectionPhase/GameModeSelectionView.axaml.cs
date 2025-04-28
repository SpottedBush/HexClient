using Avalonia.Controls;
using Avalonia.Interactivity;
using HexClientProject.Models;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.GameModeSelectionPhase;
using HexClientProject.ViewModels.ViewManagement;

namespace HexClientProject.Views
{
    public partial class GameModeSelectionView : UserControl
    {
        public GameModeSelectionView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = new GameModeSelectionViewModel(mainViewModel);
        }
    }
}