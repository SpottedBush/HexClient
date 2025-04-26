using Avalonia.Controls;
using HexClientProject.Models;
using HexClientProject.StateManagers;
using HexClientProject.ViewModels;

namespace HexClientProject.Controls
{
    public partial class LobbyPlayerControl : UserControl
    {
        private LobbyPlayerControl(int playerId)
        {
            InitializeComponent();

            var vm = new LobbyPlayerViewModel(playerId);

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
            var lobbyPlayerControl = new LobbyPlayerControl(StateManager.Instance.LobbyInfo.NbPlayers - 1);
        }
    }
}