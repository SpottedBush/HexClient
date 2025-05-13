using System.Collections.Generic;
using HexClientProject.Models;

namespace HexClientProject.Interfaces;

public interface ISummonerService
{
    /// <summary>
    /// Retrieves the current summoner's information model.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="SummonerInfoModel"/> representing the current summoner's data.
    /// </returns>
    SummonerInfoModel GetCurrentSummonerInfoModel();

    /// <summary>
    /// Retrieves the summoner's information model associated with the specified PUUID.
    /// </summary>
    /// <param name="puuid">The PUUID of the summoner whose information is to be retrieved.</param>
    /// <returns>
    /// An instance of <see cref="SummonerInfoModel"/> containing the data for the specified summoner.
    /// </returns>
    SummonerInfoModel GetSummonerInfoModel(string puuid);

    /// <summary>
    /// Retrieves a list of summoner information models based on the provided list of PUUIDs.
    /// </summary>
    /// <param name="puuidList">A list of PUUIDs representing the summoners whose information is to be retrieved.</param>
    /// <returns>
    /// A list of <see cref="SummonerInfoModel"/> instances containing the data for the specified summoners.
    /// </returns>
    List<SummonerInfoModel> GetSummonerInfoList(List<string> puuidList);
}