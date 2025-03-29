using Avalonia.Controls;
using Avalonia.Interactivity;
using HexClientProject.Models;
using HexClientProject.ViewModels;

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