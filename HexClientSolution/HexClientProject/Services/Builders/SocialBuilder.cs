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
    public class SocialBuilder
    {
        public static FriendModel? CreateFriendModel(string puuid)
        { 
            string response = SocialService.GetFriends().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            foreach (var friend in jsonObject) 
            {
                if (friend.puuid == puuid)
                {
                    string responseSiR = SummonerService.GetSummonerRankedInfos(puuid).Result;
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
        public static List<FriendModel> CreateFriendModelList()
        {
            int count = 0;
            List<FriendModel> friendModelList = new List<FriendModel>();

            // Get api response
            string response = SocialService.GetFriends().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            // Get friend puuids
            List<string> friendPuuidList = new List<string>();
            foreach (var friend in jsonObject)
            {
                friendPuuidList.Add(friend.puuid);
            }

            // Get friend summoner infos models
            List<SummonerInfoModel> friendSummonerModelList = SummonerBuilder.CreateSummonerInfoList(friendPuuidList);

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
    }
}
