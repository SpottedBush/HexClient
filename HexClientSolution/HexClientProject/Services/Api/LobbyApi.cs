using System;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using LcuApi;

namespace HexClientProject.Services.Api
{
    public static class LobbyApi
    {
        // TOTEST
        public static async System.Threading.Tasks.Task<string> GetLobbyInfos()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-lobby/v2/lobby");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get current summoner - Return code: " + response.StatusCode + " | " + responseStr);
            }
            return responseStr;
        }

        public static async System.Threading.Tasks.Task<bool> CreateLobby(string gameId)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;
            var body = new { queueId = gameId };
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/", body);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot create the lobby " + GameModeModel.GetGameModeFromGameId(gameId) + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        // TOTEST
        public static async System.Threading.Tasks.Task<bool> SetPrefPositions(string pos1, string pos2)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            var body = new { firstPreference = pos1, secondPreference = pos2 };

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Put, "lol-lobby/v2/lobby/members/localMember/position-preferences", body);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot set positions " + pos1 + " and " + pos2 + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        // TOTEST
        public static async System.Threading.Tasks.Task<bool> SendLobbyInvitation(string summonerIdToInvite)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/" + summonerIdToInvite + "/grant-invite");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot invite summoner: " + summonerIdToInvite + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        // TOTEST
        public static async System.Threading.Tasks.Task<bool> RevokeLobbyInvitation(string summonerIdToRevoke)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/" + summonerIdToRevoke + "/revoke-invite");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot invite summoner: " + summonerIdToRevoke + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        // TOTEST
        public static async System.Threading.Tasks.Task<bool> KickPlayerFromLobby(string summonerIdToKick)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/" + summonerIdToKick + "/kick");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot kick summoner: " + summonerIdToKick + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        // TOTEST
        public static async System.Threading.Tasks.Task<bool> PromotePlayer(string summonerIdToPromote)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/" + summonerIdToPromote + "/promote");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot kick summoner: " + summonerIdToPromote + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        // /!\ Need to store invitation ids
        public static async System.Threading.Tasks.Task<string> GetLobbyInvitations()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-lobby/v2/received-invitations");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get lobby invitations - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return responseStr;
        }

        // TOTEST
        // /!\ Need to store invitation ids
        public static async System.Threading.Tasks.Task<bool> AcceptLobbyInvitation(string invitationId)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/received-invitations/" + invitationId + "/accept");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot accept lobby invitation: " + invitationId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }


        // TOTEST
        // /!\ Need to store invitation ids
        public static async System.Threading.Tasks.Task<bool> RejectLobbyInvitation(string invitationId)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/received-invitations/" + invitationId + "/decline");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot decline lobby invitation: " + invitationId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        // public static async void IsLobbyOpen(string invitationId)
        // {
        //     
        // }
        
    }
}
