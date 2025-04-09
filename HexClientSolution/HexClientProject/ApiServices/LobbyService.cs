using System;
using HexClientProject.Models;
using LcuApi;

namespace HexClientProject.ApiServices
{
    public class LobbyService
    {
        // TOTEST
        public static async System.Threading.Tasks.Task<string> GetLobbyInfos()
        {
            ILeagueClient api = await LeagueClient.Connect();
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-lobby/v2/lobby");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get current summoner - Return code: " + response.StatusCode + " | " + responseStr);
            }
            return responseStr;
        }

        public static async void CreateLobby(string gameId)
        {
            ILeagueClient api = await LeagueClient.Connect();
            var body = new { queueId = gameId };
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/", body);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot create the lobby " + GameModeModel.GetGameModeFromGameId(gameId) + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        public async void CreateCustomLobby()
        {

        }

        // TOTEST
        public static async void SetPrefPositions(string pos1, string pos2)
        {
            ILeagueClient api = await LeagueClient.Connect();

            var body = new { firstPreference = pos1, secondPreference = pos2 };

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Put, "lol-lobby/v2/lobby/members/localMember/position-preferences", body);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot set positions " + pos1 + " and " + pos2 + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        // TOTEST
        public static async void SendLobbyInvitation(long summonerIdToInvite)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/" + summonerIdToInvite + "/grant-invite");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot invite summoner: " + summonerIdToInvite + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        // TOTEST
        public static async void RevokeLobbyInvitation(long summonerIdToRevoke)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/" + summonerIdToRevoke + "/revoke-invite");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot invite summoner: " + summonerIdToRevoke + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        // TOTEST
        public static async void KickPlayerFromLobby(long summonerIdToKick)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/" + summonerIdToKick + "/kick");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot kick summoner: " + summonerIdToKick + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        // TOTEST
        public static async void PromotePlayer(long summonerIdToPromote)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/" + summonerIdToPromote + "/promote");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot kick summoner: " + summonerIdToPromote + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        // /!\ Need to store intation ids
        public static async System.Threading.Tasks.Task<string> GetLobbyInvitations()
        {
            ILeagueClient api = await LeagueClient.Connect();

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
        public static async void AcceptLobbyInvitation(string invitationId)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/received-invitations/" + invitationId + "/accept");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot accept lobby invitation: " + invitationId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }


        // TOTEST
        // /!\ Need to store invitation ids
        public static async void RejectLobbyInvitation(string invitationId)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/received-invitations/" + invitationId + "/decline");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot decline lobby invitation: " + invitationId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        public static async void IsLobbyOpen(string invitationId)
        {
        }


    }
}
