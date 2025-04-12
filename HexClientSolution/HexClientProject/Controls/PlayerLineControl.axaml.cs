using System;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using HexClientProject.Models;
using HexClientProject.ViewModels;

namespace HexClientProject.Controls
{
    public partial class PlayerLineControl : UserControl
    {
        private PlayerLineViewModel vm;

        private PlayerLineControl(int playerId)
        {
            InitializeComponent();

            vm = new PlayerLineViewModel(playerId);

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

        public PlayerLineControl()
        {
            InitializeComponent();
            var playerLineControl = new PlayerLineControl(StateManager.Instance.LobbyInfo.NbPlayers - 1);
        }

        public PlayerLineViewModel ViewModel
        {
            get => (PlayerLineViewModel)DataContext;
            set => DataContext = value;
        }
    }
}