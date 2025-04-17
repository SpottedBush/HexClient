using System;
using LcuApi;

namespace HexClientProject.Services.Api
{
    public static class SummonerService
    {
        public static async System.Threading.Tasks.Task<string> GetCurrentSummonerInfos()
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

        public static async System.Threading.Tasks.Task<string> GetSummonerInfos(string puuid)
        {
            ILeagueClient api = await LeagueClient.Connect();
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-summoner/v2/summoners/puuid/" + puuid);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get the summoner: " + puuid + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
            return responseStr;
        }

        public static async System.Threading.Tasks.Task<string> GetSummonerRankedInfos(string summonerPuuid)
        {
            ILeagueClient api = await LeagueClient.Connect();
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-ranked/v1/ranked-stats/" + summonerPuuid);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get current summoner - Return code: " + response.StatusCode + " | " + responseStr);
            }
            return responseStr;
        }
    }
}
