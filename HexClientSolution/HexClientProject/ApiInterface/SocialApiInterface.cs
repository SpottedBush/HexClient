using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexClienT.Models;
using HexClientProject.Models;
using HexClientProject.ViewModels;
using Newtonsoft.Json;

namespace HexClientProject.ApiInterface
{
    public class SocialApiInterface
    {
        public static FriendModel CreateFriendModel(string puuid)
        { 
            string response = ApiServices.SocialService.GetFriends().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            foreach (var friend in jsonObject) 
            {
                if (friend.puuid == puuid)
                {
                    string responseSIR = ApiServices.SummonerService.GetSummonerRankedInfos(puuid).Result;
                    dynamic jsonObjectSIR = JsonConvert.DeserializeObject<dynamic>(responseSIR);
                    Func<dynamic, bool> filterCondition = x => x.queueType == "RANKED_SOLO_5x5";
                    dynamic queueStatsList = jsonObjectSIR.Filter(filterCondition);

                    return new FriendModel { 
                        Username = friend.gameName,
                        Status = friend.statusMessage,
                        RankId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier),
                        DivisionId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier) 
                    };
                }
            }
            return null;
        }
        public static List<FriendModel> CreateFriendModelList()
        {
            int count = 0;
            List<FriendModel> friendModelList = new List<FriendModel>();

            // Get api response
            string response = ApiServices.SocialService.GetFriends().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            // Get friend puuids
            List<string> friendPuuidList = new List<string>();
            foreach (var friend in jsonObject)
            {
                friendPuuidList.Add(friend.puuid);
            }

            // Get friend summoner infos models
            List<SummonerInfoModel> friendSummonerModelList = SummonerApiInterface.CreateSummonerInfoList(friendPuuidList);
            if (friendSummonerModelList == null)
            {
                throw new Exception("Set lobby infos: Json error");
                return null;
            }

            // Create friend models and fill the list
            foreach (var friend in jsonObject)
            {
                friendModelList.Add(new FriendModel
                {
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
