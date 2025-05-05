using System;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.SideBar;

namespace HexClientProject.Models
{
    public class FriendModel: SummonerInfoModel
    {
        public required string Status { get; set; }
        public int Level { get; set; }

        public bool IsOnline { get; set; }
        public FriendsListViewModel? ParentViewModel { get; set; }
        
    }
}