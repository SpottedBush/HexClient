using System;
using System.Diagnostics;
using Avalonia.Controls;
using HexClienT.Models;
using HexClientProject.ViewModels;
using HexClientProject.Views;
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
        public event Action LobbyStateChanged = null!;
        public event Action SummonerStateChanged = null!;
        private static StateManager? _instance;
        // Public property to access the single instance
        public static StateManager Instance
        {
            get { return _instance ??= new StateManager(); }
        }
        public LcuApi.ILeagueClient Api
        {
            get => _api;
            set => this.RaiseAndSetIfChanged(ref _api, value);
        }
        public void MockCreateLobby(string gameModeName)
        {
            Debug.Assert(_instance != null, nameof(_instance) + " != null");
            _instance.LobbyInfo = new LobbyInfoModel
            {
                LobbyName = "TouDansLTrou's Game",
                LobbyPassword = "benvoyons",
                HostName = "TouDansLTrou",
                NbPlayers = 2, // Hey, I have a friend !
                MaxPlayersLimit = 5,
                CanQueue = true,
                CurrSelectedGameModeModel = new GameModeModel(gameModeName),
                LeaderName = "TouDansLTrou",
                Summoners = [Instance.SummonerInfo,
                    new SummonerInfoModel
                    {
                        Puuid = "2e63341a-e627-48ac-bb1a-9d56e2e9cc4f",
                        SummonerId = 015205,
                        GameName = "TouDansLTrou(le2)",
                        ProfileIconId = 1,
                        TagLine = "CACA",
                        SummonerLevel = 31,
                        XpSinceLastLevel = 2000,
                        XpUntilNextLevel = 4000,
                        RankId = 1,
                        DivisionId = 2,
                        Lp = 47,
                        Region = "EUW"
                    },
                    new SummonerInfoModel
                    {
                        Puuid = "2e63341a-e627-48ac-bb1a-9d56e2e9cc4f",
                        SummonerId = 015205,
                        GameName = "TouDansLTrou(le3)",
                        ProfileIconId = 1,
                        TagLine = "CACA",
                        SummonerLevel = 31,
                        XpSinceLastLevel = 2000,
                        XpUntilNextLevel = 4000,
                        RankId = 1,
                        DivisionId = 2,
                        Lp = 47,
                        Region = "EUW"
                    }
                    ]
            };

            // Notify subscribers that the state has changed
            LobbyStateChanged?.Invoke();
        }
        
        public void UpdateLobbyInfos()
        {
            Debug.Assert(_instance != null, nameof(_instance) + " != null");
            _instance.LobbyInfo = new LobbyInfoModel();
            _instance.LobbyInfo.SetLobbyInfo();

            // Notify subscribers that the state has changed
            LobbyStateChanged?.Invoke();
        }
        public void MockSetSummonerInfo()
        {
            Debug.Assert(_instance != null, nameof(_instance) + " != null");
            _instance.SummonerInfo = new SummonerInfoModel
            {
                Puuid = "2e63341a-e627-48ac-bb1a-9d56e2e9cc4f",
                SummonerId = 015205,
                GameName = "TouDansLTrou",
                ProfileIconId = 1,
                TagLine = "CACA",
                SummonerLevel = 31,
                XpSinceLastLevel = 2000,
                XpUntilNextLevel = 4000,
                RankId = 1,
                DivisionId = 2,
                Lp = 47,
                Region = "EUW"
            };

            // Notify subscribers that the state has changed
            SummonerStateChanged?.Invoke();
        }
        public void SetSummonerInfo()
        {
            Debug.Assert(_instance != null, nameof(_instance) + " != null");
            _instance.SummonerInfo = new SummonerInfoModel();
            _instance.SummonerInfo.SetSummonerInfo();
            
            // Notify subscribers that the state has changed
            SummonerStateChanged?.Invoke();
        }
    }
}
