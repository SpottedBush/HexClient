using System.Collections.Generic;
using HexClientProject.Models;

namespace HexClientProject.Interfaces;

public interface ISocialService
{
    FriendModel? GetFriendModel(string puuid);
    List<FriendModel> GetFriendModelList();
    bool AddFriend(FriendModel friend);
    bool RemoveFriend(string usernameToRemove);
}