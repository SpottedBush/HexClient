using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using HexClientProject.Models;
using ReactiveUI;

namespace HexClientProject.ViewModels
{
    public class LobbyPlayerViewModel : ReactiveObject
    {
        private int _playerId;
        public int PlayerId
        {
            get => _playerId;
            set => this.RaiseAndSetIfChanged(ref _playerId, value);
        }

        private string _displayText = null!;
        public string DisplayText
        {
            get => _displayText;
            set => this.RaiseAndSetIfChanged(ref _displayText, value);
        }
        private Bitmap _roleIcon1 = null!;

        public Bitmap RoleIcon1
        {
            get => _roleIcon1;
            set => this.RaiseAndSetIfChanged(ref _roleIcon1, value);
        }

        private Bitmap _roleIcon2 = null!;
        public Bitmap RoleIcon2
        {
            get => _roleIcon2;
            set => this.RaiseAndSetIfChanged(ref _roleIcon2, value);
        }

        public LobbyPlayerViewModel(int playerId)
        {
            if (playerId >= StateManager.Instance.LobbyInfo.Summoners.Count)
                return;
            PlayerId = playerId;

            var summoner = StateManager.Instance.LobbyInfo.Summoners[playerId];
            DisplayText = $"{summoner.GameName} (Level {summoner.SummonerLevel}) " +
                          $"Rank: {SummonerInfoViewModel.RankStrings[summoner.RankId]} " +
                          $"{SummonerInfoViewModel.RankDivisions[summoner.DivisionId]}";

            RoleIcon1 = new Bitmap(AssetLoader.Open(new Uri($"avares://HexClientProject/Assets/roles/none_icon.png")));
            RoleIcon2 = new Bitmap(AssetLoader.Open(new Uri($"avares://HexClientProject/Assets/roles/none_icon.png")));
        }
        public void SetPlayerRole(string role1, string role2)
        {
            RoleIcon1 = new Bitmap(AssetLoader.Open(new Uri($"avares://HexClientProject/Assets/roles/{role1}_icon.png")));
            RoleIcon2 = new Bitmap(AssetLoader.Open(new Uri($"avares://HexClientProject/Assets/roles/{role2}_icon.png")));
        }
    }
}