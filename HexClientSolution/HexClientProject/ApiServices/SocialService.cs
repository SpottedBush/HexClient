using System;
using LcuApi;
using Newtonsoft.Json;

namespace HexClientProject.ApiServices
{
    public class SocialService
    {
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

        public static async System.Threading.Tasks.Task<bool> SendFriendRequest(string gameName, string gameTag)
        {
            ILeagueClient api = await LeagueClient.Connect();

            var body = new { gameName, gameTag };

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-chat/v2/friend-requests/", body);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot send friend request to" + gameName + "#" + gameTag + " - Return code: " + response.StatusCode + " | " + responseStr);
                return false;
            }

            return true;
        }

        public static async System.Threading.Tasks.Task<bool> AcceptFriendRequest(string requestId)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Put, "lol-chat/v1/friend-requests/" + requestId);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot accept friend request: " + requestId + " - Return code: " + response.StatusCode + " | " + responseStr);
                return false;
            }

            return true;
        }

        public static async System.Threading.Tasks.Task<bool> RejectFriendRequest(string requestId)
        {
            ILeagueClient api = await LeagueClient.Connect();

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-chat/v1/friend-requests/" + requestId);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot reject friend request: " + requestId + " - Return code: " + response.StatusCode + " | " + responseStr);
                return false;
            }

            return true;
        }
    }
}
