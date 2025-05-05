using System;
using HexClientProject.Services.Providers;
using LcuApi;

namespace HexClientProject.Services.Api
{
    public static class SummonerApi
    {
        public static async System.Threading.Tasks.Task<string> GetCurrentSummonerInfos()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
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
            ILeagueClient api = LcuWebSocketService.Instance().Result;
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
            ILeagueClient api = LcuWebSocketService.Instance().Result;
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
