using System.Collections.Generic;
using HexClientProject.Models;

namespace HexClientProject.Interfaces;

public interface ISocialService
{
    FriendModel? GetFriendModel(string puuid);
    List<FriendModel> GetFriendModelList();
    bool AddFriend(string newFriendUsername);
    bool RemoveFriend(string usernameToRemove);
    bool PostInviteToLobby(FriendModel friend);
    bool BlockFriend(string usernameToBlock);
    bool UnblockFriend(string usernameToBlock);
    void SendMessage(MessageModel message);
}