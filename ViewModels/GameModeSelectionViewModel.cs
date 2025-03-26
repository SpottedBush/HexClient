using HexClient.Models;

namespace HexClient.ViewModels
{
    public class GameModeSelectionViewModel : ViewModelBase
    {
        public void SelectQuickPlay()
        {
            GameMode currGameMode = new GameMode("Quick Play");
        }
    }
}