using System.Collections.ObjectModel;
using HexClienT.Models;
using HexClientProject.ViewModels;

namespace HexClientProject.Models
{
    public class FriendModel: SummonerInfoModel
    {
        public int IconId { get; set; }
        public required string? Username { get; set; }
        public required string Status { get; set; }
        public new int RankId { get; init; }
        public string RankDisplay { get; set; }

        public new int DivisionId { get; init; }
        public int Level { get; set; }

        public bool IsOnline { get; set; }
        public ObservableCollection<MessageModel> ChatMessages { get; set; } = new();

        public FriendModel()
        {
            RankDisplay = SummonerInfoViewModel.RankStrings[RankId] + " " + SummonerInfoViewModel.RankDivisions[DivisionId];
        }
        
    }
}