using Avalonia.Controls;
using Avalonia.Interactivity;
using HexClientProject.Models;

namespace HexClientProject.Views
{
    public partial class GameModeSelectionView : UserControl
    {
        public GameModeSelectionView()
        {
            InitializeComponent();
        }
        public void SelectGameMode(object sender, RoutedEventArgs e)
        {
            Button clickedButton = e.Source as Button;
            LobbyInfo.CurrSelectedGameMode = new GameMode(clickedButton.Name);

            ApiService.CreateLobby(GameMode.GetGameIdFromGameMode(clickedButton.Name));
        }
    }
}