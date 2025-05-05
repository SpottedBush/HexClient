using System;
using System.Collections.Generic;
using System.Linq;
using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.Services.Api;
using HexClientProject.ViewModels;
using Newtonsoft.Json;
using SummonerInfoViewModel = HexClientProject.ViewModels.SideBar.SummonerInfoViewModel;

namespace HexClientProject.Services.Builders
{
    public class SummonerBuilder : ISummonerService
    {
        public SummonerInfoModel GetCurrentSummonerInfoModel()
        {

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
            SummonerInfoModel summonerInfoModel = new SummonerInfoModel
            {
                Puuid = jsonObjectSi!.puuid,
                SummonerId = jsonObjectSi.summonerId,
                GameName = jsonObjectSi.gameName,
                ProfileIconId = jsonObjectSi.profileIconId,
                TagLine = jsonObjectSi.tagLine,
                SummonerLevel = jsonObjectSi.summonerLevel,
                XpSinceLastLevel = jsonObjectSi.xpSinceLastLevel,
                XpUntilNextLevel = jsonObjectSi.xpUntilNextLevel,
                RankId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier),
                DivisionId = SummonerInfoViewModel.RankDivisions.Find(queueStatsList[0].division),
                Lp = queueStatsList[0].leaguePoints
                // summonerInfoModel.Region = jsonObjectSi.region;
            };

            return summonerInfoModel;
        }


        public SummonerInfoModel GetSummonerInfoModel(string puuid)
        {
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

            SummonerInfoModel summonerInfoModel = new SummonerInfoModel
            {
                Puuid = jsonObjectSi!.puuid,
                SummonerId = jsonObjectSi.summonerId,
                GameName = jsonObjectSi.gameName,
                ProfileIconId = jsonObjectSi.profileIconId,
                TagLine = jsonObjectSi.tagLine,
                SummonerLevel = jsonObjectSi.summonerLevel,
                XpSinceLastLevel = jsonObjectSi.xpSinceLastLevel,
                XpUntilNextLevel = jsonObjectSi.xpUntilNextLevel,
                RankId = SummonerInfoViewModel.RankStrings.Find(queueStatsList[0].tier),
                DivisionId = SummonerInfoViewModel.RankDivisions.Find(queueStatsList[0].division),
                Lp = queueStatsList[0].leaguePoints
                // summonerInfoModel.Region = jsonObjectSi.region;
            };
            return summonerInfoModel;
        }

        public List<SummonerInfoModel> GetSummonerInfoList(List<string> puuidList)
        {
            List<SummonerInfoModel> summonerInfoModelList = [];
            summonerInfoModelList.AddRange(puuidList.Select(GetSummonerInfoModel));
            return summonerInfoModelList;
        }
    }
}
