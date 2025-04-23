using System;
using HexClientProject.ViewModels;

namespace HexClientProject.Models
{
    public class FriendModel: SummonerInfoModel
    {
        public int IconId { get; set; }
        public required string Username { get; set; } = string.Empty;
        public required string Status { get; set; }
        public new int RankId { get; init; }
        public string RankDisplay { get; set; }

        public new int DivisionId { get; init; }
        public int Level { get; set; }

        public bool IsOnline { get; set; }
        public FriendsListViewModel? ParentViewModel { get; set; }
        public FriendModel()
        {
            RankDisplay = SummonerInfoViewModel.RankStrings[RankId] + " " + SummonerInfoViewModel.RankDivisions[DivisionId];
        }
        
    }
}