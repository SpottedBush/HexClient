using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using HexClientProject.Models;

namespace HexClientProject.ViewModels
{
    public partial class LobbyPlayerViewModel : ObservableObject
    {
        [ObservableProperty] private int _playerId;

        [ObservableProperty] private string _displayText;

        [ObservableProperty] private Bitmap _roleIcon1;

        [ObservableProperty] private Bitmap _roleIcon2;

        public LobbyPlayerViewModel(int playerId)
        {
            if (playerId >= StateManager.Instance.LobbyInfo.Summoners!.Count)
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