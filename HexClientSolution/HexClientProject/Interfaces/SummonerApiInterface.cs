using System;
using HexClienT.Models;
using Newtonsoft.Json;
using HexClientProject.ViewModels;
using System.Collections.Generic;

namespace HexClientProject.ApiInterface
{
    public class SummonerApiInterface
    {
        public static SummonerInfoModel GetCurrentSummonerInfoModel()
        {
            SummonerInfoModel summonerInfoModel = new SummonerInfoModel();

            string responseSI = ApiServices.SummonerService.GetCurrentSummonerInfos().Result;
            dynamic jsonObjectSI = JsonConvert.DeserializeObject<dynamic>(responseSI);

            string responseSIR = ApiServices.SummonerService.GetSummonerRankedInfos(jsonObjectSI.puuid).Result;
            dynamic jsonObjectSIR = JsonConvert.DeserializeObject<dynamic>(responseSIR);

            if (jsonObjectSI == null || jsonObjectSIR == null)
            {
                throw new Exception("Set summoner infos: Json error");
                return null;
            }

            Func<dynamic, bool> filterCondition = x => x.queueType == "RANKED_SOLO_5x5";
            dynamic queueStatsList = jsonObjectSIR.Filter(filterCondition);

            summonerInfoModel.Puuid = jsonObjectSI.puuid;
            summonerInfoModel.SummonerId = jsonObjectSI.summonerId;
            summonerInfoModel.GameName = jsonObjectSI.gameName;
            summonerInfoModel.ProfileIconId = jsonObjectSI.profileIconId;
            summonerInfoModel.TagLine = jsonObjectSI.tagLine;
            summonerInfoModel.SummonerLevel = jsonObjectSI.summonerLevel;
            summonerInfoModel.XpSinceLastLevel = jsonObjectSI.xpSinceLastLevel;
            summonerInfoModel.XpUntilNextLevel = jsonObjectSI.xpUntilNextLevel;
            summonerInfoModel.RankId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier);
            summonerInfoModel.DivisionId = SummonerInfoViewModel.RankDivisions.Find(queueStatsList[0].division);
            summonerInfoModel.Lp = queueStatsList[0].leaguePoints;
            // summonerInfoModel.Region = jsonObjectSI.region;

            return summonerInfoModel;
        }


        public static SummonerInfoModel CreateSummonerInfoModel(string puuid)
        {
            SummonerInfoModel summonerInfoModel = new SummonerInfoModel();
            string responseSI = ApiServices.SummonerService.GetSummonerInfos(puuid).Result;
            dynamic jsonObjectSI = JsonConvert.DeserializeObject<dynamic>(responseSI);

            string responseSIR = ApiServices.SummonerService.GetSummonerRankedInfos(puuid).Result;
            dynamic jsonObjectSIR = JsonConvert.DeserializeObject<dynamic>(responseSIR);

            if (jsonObjectSI == null || jsonObjectSIR == null)
            {
                throw new Exception("Set summoner infos: Json error");
                return null;
            }

            Func<dynamic, bool> filterCondition = x => x.queueType == "RANKED_SOLO_5x5";
            dynamic queueStatsList = jsonObjectSIR.Filter(filterCondition);

            summonerInfoModel.Puuid = jsonObjectSI.puuid;
            summonerInfoModel.SummonerId = jsonObjectSI.summonerId;
            summonerInfoModel.GameName = jsonObjectSI.gameName;
            summonerInfoModel.ProfileIconId = jsonObjectSI.profileIconId;
            summonerInfoModel.TagLine = jsonObjectSI.tagLine;
            summonerInfoModel.SummonerLevel = jsonObjectSI.summonerLevel;
            summonerInfoModel.XpSinceLastLevel = jsonObjectSI.xpSinceLastLevel;
            summonerInfoModel.XpUntilNextLevel = jsonObjectSI.xpUntilNextLevel;
            summonerInfoModel.RankId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier);
            summonerInfoModel.DivisionId = SummonerInfoViewModel.RankDivisions.Find(queueStatsList[0].division);
            summonerInfoModel.Lp = queueStatsList[0].leaguePoints;
            // summonerInfoModel.Region = jsonObjectSI.region;

            return summonerInfoModel;
        }

        public static List<SummonerInfoModel> CreateSummonerInfoList(List<string> puuidList)
        {
            List<SummonerInfoModel> summonerInfoModelList = new List<SummonerInfoModel>();

            foreach (string puuid in puuidList)
            {
                var summonerInfoModel = CreateSummonerInfoModel(puuid);

                if (summonerInfoModel == null)
                {
                    return null;
                }

                summonerInfoModelList.Add(summonerInfoModel);
            }
            
            return summonerInfoModelList;
        }
    }
}
