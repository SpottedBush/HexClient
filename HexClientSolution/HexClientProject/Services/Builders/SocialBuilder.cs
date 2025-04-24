using System;
using System.Collections.Generic;
using System.Linq;
using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.Services.Api;
using HexClientProject.ViewModels;
using Newtonsoft.Json;

namespace HexClientProject.Services.Builders
{
    public class SocialBuilder : ISocialService
    {
        private readonly SummonerBuilder _summonerBuilder = new SummonerBuilder();
        public FriendModel? GetFriendModel(string puuid)
        { 
            string response = SocialApi.GetFriends().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            foreach (var friend in jsonObject) 
            {
                if (friend.puuid == puuid)
                {
                    string responseSiR = SummonerApi.GetSummonerRankedInfos(puuid).Result;
                    dynamic? jsonObjectSiR = JsonConvert.DeserializeObject<dynamic>(responseSiR);
                    bool FilterCondition(dynamic x) => x.queueType == "RANKED_SOLO_5x5";
                    if (jsonObjectSiR != null)
                    {
                        dynamic queueStatsList = jsonObjectSiR.Filter((Func<dynamic, bool>)FilterCondition);

                        return new FriendModel { 
                            Username = friend.gameName,
                            Status = friend.statusMessage,
                            RankId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier),
                            DivisionId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier) 
                        };
                    }
                }
            }
            return null;
        }

        public string GetFriendPuuidFromName(string friendUserName)
        {
            string response = SocialApi.GetFriends().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();
            foreach (var friend in jsonObject)
            {
                if (friend.gameName + "#" + friend.gameTag == friendUserName)
                {
                    return friend.puuid;
                }
            }
            return "";
        }

        public List<FriendModel> GetFriendModelList()
        {
            int count = 0;
            List<FriendModel> friendModelList = new List<FriendModel>();

            // Get api response
            string response = SocialApi.GetFriends().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            // Get friend puuids
            List<string> friendPuuidList = new List<string>();
            foreach (var friend in jsonObject)
            {
                friendPuuidList.Add(friend.puuid);
            }

            // Get friend summoner infos models
            List<SummonerInfoModel> friendSummonerModelList = _summonerBuilder.GetSummonerInfoList(friendPuuidList);

            // Create friend models and fill the list
            foreach (var friend in jsonObject)
            {
                friendModelList.Add(new FriendModel
                {
                    Puuid = friend.puuid,
                    Username = friendSummonerModelList[count].GameName,
                    Status = friend.statusMessage,
                    RankId = friendSummonerModelList[count].RankId,
                    DivisionId = friendSummonerModelList[count].DivisionId
                });
                count++;
            }

            return friendModelList;
        }

        public List<string> GetMutedUserList()
        {
            throw new NotImplementedException();
        }

        public bool ViewProfile(string username)
        {
            throw new NotImplementedException();
        }

        public bool AddFriend(string newFriendUsername)
        {
            string[] nameAndTag = newFriendUsername.Split('#');
            
            if (nameAndTag.Length != 2)
            {
                throw new ArgumentException("Invalid username format. Expected format: 'Name#Tag'.");
                return false;
            }
            string name = nameAndTag[0];
            string tag = nameAndTag[1];

            return SocialApi.SendFriendRequest(name, tag).Result;
        }

        public bool RemoveFriend(string usernameToRemove)
        {
            string puuid = GetFriendPuuidFromName(usernameToRemove);
            if (string.IsNullOrEmpty(puuid))
            {
                return false;
                throw new ArgumentException("Invalid username format. Expected format: 'Name#Tag'.");
            }
            return SocialApi.RemoveFriend(puuid).Result;
        }

        public bool PostInviteToLobby(FriendModel friend)
        {
            if (friend == null)
            {
                return false;
                throw new ArgumentNullException(nameof(friend), "Friend model cannot be null.");
            }
            if (string.IsNullOrEmpty(friend.Puuid))
            {
                return false;
                throw new ArgumentException("Friend model must have a valid Puuid.");
            }
            return LobbyApi.SendLobbyInvitation(friend.Puuid).Result;
        }

        public List<SummonerInfoModel> GetBlockedPlayers()
        {
            string response = SocialApi.GetBlockedPlayer().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            if (string.IsNullOrEmpty(response))
            {
                return new List<SummonerInfoModel>();
                throw new Exception("Get blocked players: Json error");
            }

            List<SummonerInfoModel> blockedPlayers = new List<SummonerInfoModel>();
            foreach (var player in jsonObject)
            {
                blockedPlayers.Add(new SummonerInfoModel
                {
                    Puuid = player.puuid,
                    SummonerId = player.summonerId,
                    GameName = player.gameName,
                    ProfileIconId = player.icon,
                    TagLine = player.gameTag,
                });
            }

            return blockedPlayers;
        }

        public bool BlockFriend(string usernameToBlock)
        {
            if (string.IsNullOrEmpty(usernameToBlock))
            {
                return false;
                throw new ArgumentException("Invalid username format. Expected format: 'Name#Tag'.");
            }

            string puuid = GetFriendPuuidFromName(usernameToBlock);
            if (string.IsNullOrEmpty(puuid))
            {
                return false;
                throw new ArgumentException("Invalid username format. Expected format: 'Name#Tag'.");
            }

            return SocialApi.BlockPlayer(puuid).Result;
        }

        public bool UnblockFriend(string usernameToUnblock)
        {
            if (string.IsNullOrEmpty(usernameToUnblock))
            {
                return false;
                throw new ArgumentException("Invalid username format. Expected format: 'Name#Tag'.");
            }

            string puuid = GetFriendPuuidFromName(usernameToUnblock);
            if (string.IsNullOrEmpty(puuid))
            {
                return false;
                throw new ArgumentException("Invalid username format. Expected format: 'Name#Tag'.");
            }

            return SocialApi.UnblockPlayer(puuid).Result;
        }

        public bool MuteUser(string usernameToMute)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(MessageModel message)
        {
            throw new NotImplementedException();
        }
    }
}
