using System.Collections.ObjectModel;
using Avalonia.Controls;
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
        
        private UserControl _currView = null!;
        public UserControl CurrView
        {
            get => _currView;
            set => this.RaiseAndSetIfChanged(ref _currView, value);
        }

        public bool IsOnlineMode;
        public LobbyInfoModel LobbyInfo { get; set; } = null!;
        public SummonerInfoModel SummonerInfo { get; set; } = null!;
        public ObservableCollection<FriendModel> Friends { get; } = new();
        public Collection<string> MutedUsernames { get; } = new();
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
