using System.Collections.Generic;
using HexClienT.Models;

namespace HexClientProject.Interfaces;

public interface ISummonerService
{
    SummonerInfoModel GetCurrentSummonerInfoModel();
    SummonerInfoModel GetSummonerInfoModel(string puuid);
    List<SummonerInfoModel> GetSummonerInfoList(List<string> puuidList);
}