using System;
using System.Collections.Generic;
using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.Services.Api;
using HexClientProject.ViewModels;
using Newtonsoft.Json;
using SummonerInfoViewModel = HexClientProject.ViewModels.SideBar.SummonerInfoViewModel;

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
                            GameName = friend.gameName,
                            TagLine = friend.gameTag,
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
                    GameName = friendSummonerModelList[count].GameName,
                    TagLine = friend.gameTag,
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
            }
            return SocialApi.RemoveFriend(puuid).Result;
        }

        public bool PostInviteToLobby(FriendModel friend)
        {
            return !string.IsNullOrEmpty(friend.Puuid) && LobbyApi.SendLobbyInvitation(friend.Puuid).Result;
        }

        public List<SummonerInfoModel> GetBlockedPlayers()
        {
            string response = SocialApi.GetMutedPlayer().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            if (string.IsNullOrEmpty(response))
            {
                return [];
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
            }

            string puuid = GetFriendPuuidFromName(usernameToBlock);
            return !string.IsNullOrEmpty(puuid) && SocialApi.BlockPlayer(puuid).Result;
        }

        public bool UnblockFriend(string usernameToUnblock)
        {
            if (string.IsNullOrEmpty(usernameToUnblock))
            {
                return false;
            }

            string puuid = GetFriendPuuidFromName(usernameToUnblock);
            return !string.IsNullOrEmpty(puuid) && SocialApi.UnblockPlayer(puuid).Result;
        }

        public bool MuteUser(string usernameToMute)
        {
            if (string.IsNullOrEmpty(usernameToMute)) 
            {
                return false;
                throw new ArgumentException("Invalid username format. Expected format: 'Name#Tag'.");
            }

            string puuid = GetFriendPuuidFromName(usernameToMute);
            if (string.IsNullOrEmpty(puuid))
            {
                return false;
                throw new ArgumentException("Invalid username format. Expected format: 'Name#Tag'.");
            }

            return SocialApi.MutePlayer(puuid).Result;
        }

        public string GetMuteUser()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(MessageModel message)
        {
            throw new NotImplementedException();
        }

        public string GetConvIdFromName(string friendUserName)
        {
            //string response = SocialApi.GetFriends().Result;
            //dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();
            //foreach (var friend in jsonObject)
            //{
            //    if (friend.gameName + "#" + friend.gameTag == friendUserName)
            //    {
            //        return friend.puuid;
            //    }
            //}
            return "";
        }
    }
}
