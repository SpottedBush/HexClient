using Avalonia.Controls;
using HexClientProject.StateManagers;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.LobbyPhase;
using SummonerInfoViewModel = HexClientProject.ViewModels.SideBar.SummonerInfoViewModel;

namespace HexClientProject.Controls
{
    public partial class LobbyPlayerControl : UserControl
    {
        private LobbyPlayerControl(int playerId)
        {
            InitializeComponent();

            var vm = new LobbyPlayerViewModel(playerId);

            GlobalStateManager globalStateManager = GlobalStateManager.Instance;

            if (playerId > globalStateManager.LobbyInfo.Summoners.Count || playerId == 0)
                return;
            var currSummoner = globalStateManager.LobbyInfo.Summoners[playerId];
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
            var lobbyPlayerControl = new LobbyPlayerControl(GlobalStateManager.Instance.LobbyInfo.NbPlayers - 1);
        }
    }
}