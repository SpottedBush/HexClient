using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using HexClientProject.Models;

namespace HexClientProject.ViewModels
{
    public partial class PlayerLineViewModel : ObservableObject
    {
        [ObservableProperty] private int playerId;

        [ObservableProperty] private string displayText;

        [ObservableProperty] private Bitmap roleIcon1;

        [ObservableProperty] private Bitmap roleIcon2;

        public PlayerLineViewModel(int playerId)
        {
            if (playerId >= StateManager.Instance.LobbyInfo.Summoners!.Count)
                return;
            PlayerId = playerId;

            var summoner = StateManager.Instance.LobbyInfo.Summoners[playerId];
            DisplayText = $"{summoner.GameName} (Level {summoner.SummonerLevel}) " +
                          $"Rank: {SummonerInfoViewModel.RankStrings[summoner.RankId]} " +
                          $"{SummonerInfoViewModel.RankDivisions[summoner.DivisionId]}";

            RoleIcon1 = new Bitmap(AssetLoader.Open(new Uri($"avares://HexClientProject/Assets/roles/autofill_icon.png")));
            RoleIcon2 = new Bitmap(AssetLoader.Open(new Uri($"avares://HexClientProject/Assets/roles/autofill_icon.png")));
        }
    }
}