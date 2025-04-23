using System.Collections.Generic;
using HexClientProject.Models;

namespace HexClientProject.Interfaces;

public interface ISummonerService
{
    SummonerInfoModel GetCurrentSummonerInfoModel();
    SummonerInfoModel GetSummonerInfoModel(string puuid);
    List<SummonerInfoModel> GetSummonerInfoList(List<string> puuidList);
}