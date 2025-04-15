using System;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using HexClientProject.Models;
using HexClientProject.ViewModels;

namespace HexClientProject.Controls
{
    public partial class LobbyPlayerControl : UserControl
    {
        private LobbyPlayerViewModel vm;

        private LobbyPlayerControl(int playerId)
        {
            InitializeComponent();

            vm = new LobbyPlayerViewModel(playerId);

            var stateManager = StateManager.Instance;

            if (playerId > stateManager.LobbyInfo.Summoners!.Count || playerId == 0)
                return;
            var currSummoner = stateManager.LobbyInfo.Summoners[playerId];
            string summonerName = currSummoner.GameName;
            int summonerLevel = currSummoner.SummonerLevel;
            string summonerRank = SummonerInfoViewModel.RankStrings[currSummoner.RankId];
            string summonerDivision = SummonerInfoViewModel.RankDivisions[currSummoner.DivisionId];
            vm.DisplayText = $"{summonerName} (Level {summonerLevel}) Rank: {summonerRank} {summonerDivision}";

            DataContext = vm;
        }

        public LobbyPlayerControl()
        {
            InitializeComponent();
            var LobbyPlayerControl = new LobbyPlayerControl(StateManager.Instance.LobbyInfo.NbPlayers - 1);
        }

        public LobbyPlayerViewModel ViewModel
        {
            get => (LobbyPlayerViewModel)DataContext;
            set => DataContext = value;
        }
    }
}