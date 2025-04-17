using System;
using System.Collections.Generic;
using HexClienT.Models;
using HexClientProject.Services.Api;
using HexClientProject.ViewModels;
using Newtonsoft.Json;

namespace HexClientProject.Interfaces
{
    public interface SummonerBuilder
    {
        public static SummonerInfoModel GetCurrentSummonerInfoModel()
        {
            SummonerInfoModel summonerInfoModel = new SummonerInfoModel();

            string responseSi = SummonerService.GetCurrentSummonerInfos().Result;
            dynamic jsonObjectSi = JsonConvert.DeserializeObject<dynamic>(responseSi)
                                   ?? throw new InvalidOperationException("Could not get current summoner infos");

            string responseSiR = SummonerService.GetSummonerRankedInfos(jsonObjectSi.puuid).Result;
            dynamic jsonObjectSiR = JsonConvert.DeserializeObject<dynamic>(responseSiR)
                                    ?? throw new InvalidOperationException("Could not get current summoner ranked infos");

            if (jsonObjectSi == null || jsonObjectSiR == null)
            {
                throw new Exception("Set summoner infos: Json error");
            }

            bool FilterCondition(dynamic x) => x.queueType == "RANKED_SOLO_5x5";
            dynamic queueStatsList = jsonObjectSiR!.Filter((Func<dynamic, bool>)FilterCondition);

            summonerInfoModel.Puuid = jsonObjectSi!.puuid;
            summonerInfoModel.SummonerId = jsonObjectSi.summonerId;
            summonerInfoModel.GameName = jsonObjectSi.gameName;
            summonerInfoModel.ProfileIconId = jsonObjectSi.profileIconId;
            summonerInfoModel.TagLine = jsonObjectSi.tagLine;
            summonerInfoModel.SummonerLevel = jsonObjectSi.summonerLevel;
            summonerInfoModel.XpSinceLastLevel = jsonObjectSi.xpSinceLastLevel;
            summonerInfoModel.XpUntilNextLevel = jsonObjectSi.xpUntilNextLevel;
            summonerInfoModel.RankId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier);
            summonerInfoModel.DivisionId = SummonerInfoViewModel.RankDivisions.Find(queueStatsList[0].division);
            summonerInfoModel.Lp = queueStatsList[0].leaguePoints;
            // summonerInfoModel.Region = jsonObjectSi.region;

            return summonerInfoModel;
        }


        private static SummonerInfoModel CreateSummonerInfoModel(string puuid)
        {
            SummonerInfoModel summonerInfoModel = new SummonerInfoModel();
            string responseSi = SummonerService.GetSummonerInfos(puuid).Result;
            dynamic? jsonObjectSi = JsonConvert.DeserializeObject<dynamic>(responseSi);

            string responseSiR = SummonerService.GetSummonerRankedInfos(puuid).Result;
            dynamic? jsonObjectSiR = JsonConvert.DeserializeObject<dynamic>(responseSiR);

            if (jsonObjectSi == null || jsonObjectSiR == null)
            {
                throw new Exception("Set summoner infos: Json error");
            }

            bool FilterCondition(dynamic x) => x.queueType == "RANKED_SOLO_5x5";
            dynamic queueStatsList = jsonObjectSiR!.Filter((Func<dynamic, bool>)FilterCondition);

            summonerInfoModel.Puuid = jsonObjectSi!.puuid;
            summonerInfoModel.SummonerId = jsonObjectSi.summonerId;
            summonerInfoModel.GameName = jsonObjectSi.gameName;
            summonerInfoModel.ProfileIconId = jsonObjectSi.profileIconId;
            summonerInfoModel.TagLine = jsonObjectSi.tagLine;
            summonerInfoModel.SummonerLevel = jsonObjectSi.summonerLevel;
            summonerInfoModel.XpSinceLastLevel = jsonObjectSi.xpSinceLastLevel;
            summonerInfoModel.XpUntilNextLevel = jsonObjectSi.xpUntilNextLevel;
            summonerInfoModel.RankId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier);
            summonerInfoModel.DivisionId = SummonerInfoViewModel.RankDivisions.Find(queueStatsList[0].division);
            summonerInfoModel.Lp = queueStatsList[0].leaguePoints;
            // summonerInfoModel.Region = jsonObjectSi.region;

            return summonerInfoModel;
        }

        public static List<SummonerInfoModel> CreateSummonerInfoList(List<string> puuidList)
        {
            List<SummonerInfoModel> summonerInfoModelList = new List<SummonerInfoModel>();

            foreach (string puuid in puuidList)
            {
                var summonerInfoModel = CreateSummonerInfoModel(puuid);

                summonerInfoModelList.Add(summonerInfoModel);
            }
            
            return summonerInfoModelList;
        }
    }
}
