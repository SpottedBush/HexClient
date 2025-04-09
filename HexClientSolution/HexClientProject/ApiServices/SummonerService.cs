using System;
using LcuApi;

namespace HexClientProject.ApiServices
{
    public class SummonerService
    {
        public static async System.Threading.Tasks.Task<string> GetSummonerInfos()
        {
            ILeagueClient api = await LeagueClient.Connect();
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-summoner/v1/current-summoner");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get current summoner - Return code: " + response.StatusCode + " | " + responseStr);
            }
            return responseStr;
        }
    }
}
