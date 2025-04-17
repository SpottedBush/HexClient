using System.Collections.ObjectModel;
using Avalonia.Controls;
using HexClienT.Models;
using HexClientProject.ViewModels;
using ReactiveUI;

namespace HexClientProject.Models
{
    public class StateManager : ReactiveObject
    {
        private UserControl _leftPanelContent = null!;

        public UserControl LeftPanelContent
        {
            get => _leftPanelContent;
            set => this.RaiseAndSetIfChanged(ref _leftPanelContent, value);
        }

        public bool IsOnlineMode;
        public LobbyInfoModel LobbyInfo { get; set; } = null!;
        public SummonerInfoModel SummonerInfo { get; set; } = null!;
        public ObservableCollection<FriendModel> Friends { get; set; }
        public ChatBoxViewModel ChatBoxViewModel { get; set; } = null!;

        private static StateManager? _instance;

        // Public property to access the single instance
        public static StateManager Instance
        {
            get
            {
                _instance ??= new StateManager();
                return _instance;
            }
        }
    }
}
