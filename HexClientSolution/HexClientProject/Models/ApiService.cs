using System;
using LcuApi;

namespace HexClientProject.Models
{

    public class ApiService
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
                throw new Exception("Err: Cannot create the lobby " + GameMode.GetGameModeFromGameId(gameId) + " - Return code: " + response.StatusCode + " | " + responseStr);
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

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/"+ summonerIdToInvite + "/grant-invite");
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

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/members/"+ summonerIdToRevoke + "/revoke-invite");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot invite summoner: " + summonerIdToRevoke + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        // /!\ Need to store intation ids
        public static async void GetLobbyInvitations()
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-lobby/v2/received-invitations");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get lobby invitations - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        // TOTEST
        // /!\ Need to store intation ids
        public static async void AcceptLobbyInvitation(string invitationId)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/received-invitations/"+invitationId+"/accept");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot accept lobby invitation: " + invitationId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }


        // TOTEST
        // /!\ Need to store intation ids
        public static async void DeclineLobbyInvitation(string invitationId)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/received-invitations/" + invitationId + "/decline");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot decline lobby invitation: " + invitationId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }
    }
    

}
