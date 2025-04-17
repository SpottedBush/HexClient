using System;
using System.Collections.Generic;
using HexClienT.Models;
using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.Services.Api;
using Newtonsoft.Json;

namespace HexClientProject.Services.Builders
{
    public class LobbyBuilder
    {
        public static LobbyInfoModel CreateLobbyInfoModel()
        {
            LobbyInfoModel lobbyInfoModel = new LobbyInfoModel();

            string response = LobbyService.GetLobbyInfos().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            if (jsonObject == null)
            {
                throw new Exception("Set lobby infos: Json error");
            }

            lobbyInfoModel.LobbyName = jsonObject.gameConfig.customLobbyName;
            lobbyInfoModel.LobbyPassword =""; // TODO Handle password when creating a custom lobby

            foreach (var m in jsonObject.members)
            {
                if (m.isLeader)
                {
                    lobbyInfoModel.LeaderName = (m.summonerId).ToString();
                    break;
                }

            }

            lobbyInfoModel.NbPlayers = jsonObject["members"].Count();
            lobbyInfoModel.MaxPlayersLimit = jsonObject.gameConfig.maxLobbySize;
            lobbyInfoModel.CanQueue = jsonObject.canStartActivity;
            lobbyInfoModel.CurrSelectedGameModeModel = new GameModeModel(GameModeModel.GetGameModeFromGameId(jsonObject.gameConfig.queueId));
            
            var sumPuuidList = new List<string>();

            foreach (var m in jsonObject.members)
            {
                sumPuuidList.Add(m.puuid);
            }

            List<SummonerInfoModel> sumList = SummonerBuilder.CreateSummonerInfoList(sumPuuidList);
            
            if (sumList == null)
            {
                throw new Exception("Set lobby infos: Json error");
            }

            lobbyInfoModel.Summoners = sumList;
            return lobbyInfoModel;
        }
    }
}
