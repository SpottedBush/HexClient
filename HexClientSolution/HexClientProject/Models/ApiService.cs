using System;
using LcuApi;
using Newtonsoft.Json;

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

        public static async System.Threading.Tasks.Task<string> GetFriends()
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/friends");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get list of friends - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return responseStr;
        }

        public static async System.Threading.Tasks.Task<string> GetFriendGroups()
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/friend-groups");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get list of friend groups - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return responseStr;
        }

        public static async System.Threading.Tasks.Task<string> GetFriendRequestsIN()
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/friend-requests/");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get incoming friend request - Return code: " + response.StatusCode + " | " + responseStr);
            }

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(responseStr) ?? throw new InvalidOperationException();
            Func<dynamic, bool> filterCondition = x => x.direction == "in";

            dynamic jsonResp = jsonObject.Filter(filterCondition);

            return JsonConvert.SerializeObject(jsonObject);
        }

        public static async System.Threading.Tasks.Task<string> GetFriendRequestsOUT()
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/friend-requests/");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get outcoming friend request - Return code: " + response.StatusCode + " | " + responseStr);
            }

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(responseStr) ?? throw new InvalidOperationException();
            Func<dynamic, bool> filterCondition = x => x.direction == "out";

            dynamic jsonResp = jsonObject.Filter(filterCondition);

            return JsonConvert.SerializeObject(jsonObject);
        }

        public static async void SendFriendRequest(string gameName, string gameTag)
        {
            ILeagueClient api = await LeagueClient.Connect();

            var body = new { gameName = gameName, gameTag = gameTag };

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-chat/v2/friend-requests/", body);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot send friend request to" + gameName + "#" + gameTag + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        public static async void AcceptFriendRequest(string requestId)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Put, "lol-chat/v1/friend-requests/" + requestId);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot accept friend request: " + requestId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }

        public static async void RejectFriendRequest(string requestId)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-chat/v1/friend-requests/" + requestId);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot reject friend request: " + requestId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }


    }
}
