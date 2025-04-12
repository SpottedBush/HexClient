using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using HexClienT.Models;
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

        public LobbyInfoModel LobbyInfo { get; set; } = null!;
        public SummonerInfoModel SummonerInfo { get; set; } = null!;
        
        private ObservableCollection<FriendModel> _friends = new();
        public ReadOnlyCollection<FriendModel> Friends { get; set; }

        public event Action LobbyStateChanged = null!;
        public event Action SummonerStateChanged = null!;

        private static StateManager? _instance;

        // Public property to access the single instance
        public static StateManager Instance
        {
            get
            {
                _instance ??= new StateManager();
                _instance.Friends = new ReadOnlyCollection<FriendModel>(_instance._friends);
                return _instance;
            }
        }

        public LcuApi.ILeagueClient Api
        {
            get => _api;
            set => this.RaiseAndSetIfChanged(ref _api, value);
        }


        public void UpdateLobbyInfos()
        {
            Debug.Assert(_instance != null, nameof(_instance) + " != null");
            _instance.LobbyInfo = new LobbyInfoModel();
            _instance.LobbyInfo.SetLobbyInfo();

            // Notify subscribers that the state has changed
            LobbyStateChanged?.Invoke();
        }

        public void SetSummonerInfo()
        {
            Debug.Assert(_instance != null, nameof(_instance) + " != null");
            _instance.SummonerInfo = new SummonerInfoModel();
            _instance.SummonerInfo.SetSummonerInfo();

            // Notify subscribers that the state has changed
            SummonerStateChanged?.Invoke();
        }

        public void ReplaceFriends(List<FriendModel> newFriends)
        {
            _friends.Clear();
            foreach (var friend in newFriends)
                _friends.Add(friend);
        }

        public void AddFriend(FriendModel newFriend)
        {
            _friends.Add(newFriend);
        }

        public Task<List<FriendModel>> GetFriendsAsync()
        {
            return Task.Run(MockingApiService.GetFriends);
        }

        public Task<bool> AddFriendAsync(string username)
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
