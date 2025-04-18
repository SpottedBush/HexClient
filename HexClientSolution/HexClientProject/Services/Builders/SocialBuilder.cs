using System;
using System.Collections.Generic;
using HexClienT.Models;
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

        public bool AddFriend(string newFriendUsername)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFriend(string usernameToRemove)
        {
            throw new NotImplementedException();
        }
    }
}
