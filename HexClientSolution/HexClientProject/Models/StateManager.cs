using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using HexClienT.Models;
using HexClientProject.ViewModels;
using ReactiveUI;

namespace HexClientProject.Models
{
    public class StateManager : ReactiveObject
    {
        private LcuApi.ILeagueClient _api = null!;
        private UserControl _leftPanelContent = null!;

        public UserControl LeftPanelContent
        {
            get => _leftPanelContent;
            set => this.RaiseAndSetIfChanged(ref _leftPanelContent, value);
        }

        public bool IsOnlineMode;
        public LobbyInfoModel LobbyInfo { get; set; } = null!;
        public SummonerInfoModel SummonerInfo { get; set; } = null!;
        
        private ObservableCollection<FriendModel> _friends = new();
        public ReadOnlyObservableCollection<FriendModel> Friends { get; set; }
        public ChatBoxViewModel ChatBoxViewModel { get; set; } = null!;

        private static StateManager? _instance;

        // Public property to access the single instance
        public static StateManager Instance
        {
            get
            {
                _instance ??= new StateManager();
                _instance.Friends = new ReadOnlyObservableCollection<FriendModel>(_instance._friends);
                return _instance;
            }
        }
        public async Task LoadFriendsAsync()
        {
            Task<List<FriendModel>> taskFriends;
            if (_instance is { IsOnlineMode: false })
            {
                taskFriends = Task.Run(MockingApiService.GetFriends);
            }
            else
            {
                taskFriends = Task.Run(MockingApiService.GetFriends); // TODO LOUIS: Add API Logic
            }
            _friends = new ObservableCollection<FriendModel>(await taskFriends);
            Friends = new ReadOnlyObservableCollection<FriendModel>(_friends);
        }

        public Task<bool> AddFriendAsync(string? username)
        {
            return Task.Run(() => MockingApiService.AddFriend(new FriendModel
            {
                Username = username,
                Status = "RIVEN OTP",
                RankId = 1,
                DivisionId = 2
            }));
        }
    }
}
