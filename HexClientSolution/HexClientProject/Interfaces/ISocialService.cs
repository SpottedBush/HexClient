using System.Collections.Generic;
using HexClientProject.Models;

namespace HexClientProject.Interfaces;

public interface ISocialService
{
    FriendModel? GetFriendModel(string puuid);
    List<FriendModel> GetFriendModelList();
    public List<string> GetMutedUserList();
    bool ViewProfile(string username); // TODO: Change the return type to "ProfileViewModel" or some stuff like this.
    bool AddFriend(string newFriendUsername);
    bool RemoveFriend(string usernameToRemove);
    bool PostInviteToLobby(FriendModel friend);
    bool BlockFriend(string usernameToBlock);
    bool UnblockFriend(string usernameToUnblock);
    bool MuteUser(string usernameToMute);
    void SendMessage(MessageModel message);
    
}