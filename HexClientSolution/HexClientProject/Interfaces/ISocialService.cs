using System.Collections.Generic;
using HexClientProject.Models;

namespace HexClientProject.Interfaces;

public interface ISocialService
{
    /// <summary>
    /// Retrieves a single friend's information based on the provided player unique identifier (PUUID).
    /// </summary>
    /// <param name="puuid">The unique identifier of the player whose friend information is being requested.</param>
    /// <returns>A <see cref="FriendModel"/> object representing the friend's information, or null if the friend is not found.</returns>
    FriendModel? GetFriendModel(string puuid);

    /// <summary>
    /// Retrieves all friends information from the LCU as a list of <see cref="FriendModel"/>.
    /// </summary>
    /// <returns>
    /// A list of <see cref="FriendModel"/> objects representing the friends associated with the current user.
    /// </returns>
    List<FriendModel> GetFriendModelList();

    /// <summary>
    /// Retrieves the list of usernames of users who are currently muted by the player.
    /// </summary>
    /// <returns>
    /// A list of strings, each representing the username of a muted user.
    /// </returns>
    public List<string> GetMutedUserList();

    
    bool ViewProfile(string username); // TODO: Change the return type to "ProfileViewModel" or some stuff like this.

    /// <summary>
    /// Sends a friend request to a user identified by their in-game username and tag.
    /// </summary>
    /// <param name="newFriendUsername">
    /// The username of the friend to add, including the tag (e.g., "Username#Tag").
    /// </param>
    /// <returns>
    /// A boolean indicating whether the friend was successfully added.
    /// Returns true if the friend request was sent successfully; otherwise, false.
    /// </returns>
    bool AddFriend(string newFriendUsername);

    /// <summary>
    /// Removes a friend from the user's friend list based on the provided username or game name tag.
    /// </summary>
    /// <param name="usernameToRemove">
    /// The username or game name tag of the friend to be removed, including the tag (e.g., "Username#Tag").
    /// </param>
    /// <returns>A boolean value indicating whether the friend was successfully removed.</returns>
    bool RemoveFriend(string usernameToRemove);

    /// <summary>
    /// Sends an invitation to the specified friend to join the user's lobby.
    /// </summary>
    /// <param name="friend">A <see cref="FriendModel"/> object representing the friend to invite to the lobby.</param>
    /// <returns>True if the invitation was successfully sent; otherwise, false.</returns>
    bool PostInviteToLobby(FriendModel friend);

    /// <summary>
    /// Blocks a specified user by their username, preventing further interactions and optionally removing them as a friend.
    /// </summary>
    /// <param name="usernameToBlock">
    /// The username of the user to block, including the tag (e.g., "Username#Tag").
    /// </param>
    /// <returns>True if the user was successfully blocked, otherwise false.</returns>
    bool BlockFriend(string usernameToBlock);

    /// <summary>
    /// Unblocks a previously blocked friend based on their username.
    /// </summary>
    /// <param name="usernameToUnblock">
    /// The username of the friend to unblock, including the tag (e.g., "Username#Tag").
    /// </param>
    /// <returns>
    /// A boolean value indicating whether the friend was successfully unblocked.
    /// Returns true if the action was successful, otherwise false.
    /// </returns>
    bool UnblockFriend(string usernameToUnblock);

    /// <summary>
    /// Mutes a user based on their username, preventing further message notifications from that user.
    /// </summary>
    /// <param name="usernameToMute">
    /// The username of the user to mute, including the tag (e.g., "Username#Tag").
    /// </param>
    /// <returns>A boolean value indicating whether the user was successfully muted.</returns>
    bool MuteUser(string usernameToMute);

    /// <summary>
    /// Sends a message using the provided message model.
    /// </summary>
    /// <param name="message">The <see cref="MessageModel"/> representing the message to be sent, including sender, content, timestamp, and scope.</param>
    void SendMessage(MessageModel message);
}