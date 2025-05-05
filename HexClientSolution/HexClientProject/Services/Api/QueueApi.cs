using System;
using HexClientProject.Services.Providers;
using LcuApi;

namespace HexClientProject.Services.Api
{
    public static class QueueApi
    {
        public static async void StartQueue()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/matchmaking/search/");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot start queue - Return code: " + response.StatusCode);
            }
        }

        public static async void StopQueue()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-lobby/v2/lobby/matchmaking/search/");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot stop queue - Return code: " + response.StatusCode);
            }
        }

        public static async System.Threading.Tasks.Task<string> GetQueueState()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-lobby/v2/lobby/matchmaking/search-state/");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get queue state - Return code: " + response.StatusCode + " | " + responseStr);
            }
            // Queue states = ["Service Shutdown", "ServiceError", "Error",
            //                 "Found", "Searching", "Canceled", "AbandonedLowPriorityQueue", "Invalid"]
            return responseStr;
        }

        public static async void AcceptQueueMatch()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-matchmaking/v1/ready-check/accept/");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot accept match - Return code: " + response.StatusCode);
            }
        }

        public static async void DeclineQueueMatch()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-matchmaking/v1/ready-check/decline/");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot decline match - Return code: " + response.StatusCode);
            }
        }
        public static async System.Threading.Tasks.Task<string> GetReadyCheckStatus()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-matchmaking/v1/ready-check");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get Ready check status - Return code: " + response.StatusCode + " | " + responseStr);
            }

            // Ready check states = ["Error", "PartyNotReady", "StrangerNotReady", "EveryoneReady", "InProgress", "Invalid"]
            return responseStr;
        }
    }
}

