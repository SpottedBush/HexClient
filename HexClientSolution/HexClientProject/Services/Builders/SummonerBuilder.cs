using System;
using System.Collections.Generic;
using HexClienT.Models;
using HexClientProject.Interfaces;
using HexClientProject.Services.Api;
using HexClientProject.ViewModels;
using Newtonsoft.Json;

namespace HexClientProject.Services.Builders
{
    public class SummonerBuilder : ISummonerService
    {
        public SummonerInfoModel GetCurrentSummonerInfoModel()
        {
            SummonerInfoModel summonerInfoModel = new SummonerInfoModel();

            string responseSi = SummonerApi.GetCurrentSummonerInfos().Result;
            dynamic jsonObjectSi = JsonConvert.DeserializeObject<dynamic>(responseSi)
                                   ?? throw new InvalidOperationException("Could not get current summoner infos");

            string responseSiR = SummonerApi.GetSummonerRankedInfos(jsonObjectSi.puuid).Result;
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


        public SummonerInfoModel GetSummonerInfoModel(string puuid)
        {
            SummonerInfoModel summonerInfoModel = new SummonerInfoModel();
            string responseSi = SummonerApi.GetSummonerInfos(puuid).Result;
            dynamic? jsonObjectSi = JsonConvert.DeserializeObject<dynamic>(responseSi);

            string responseSiR = SummonerApi.GetSummonerRankedInfos(puuid).Result;
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

        public List<SummonerInfoModel> GetSummonerInfoList(List<string> puuidList)
        {
            List<SummonerInfoModel> summonerInfoModelList = new List<SummonerInfoModel>();

            foreach (string puuid in puuidList)
            {
                var summonerInfoModel = GetSummonerInfoModel(puuid);

                summonerInfoModelList.Add(summonerInfoModel);
            }
            
            return summonerInfoModelList;
        }
    }
}
