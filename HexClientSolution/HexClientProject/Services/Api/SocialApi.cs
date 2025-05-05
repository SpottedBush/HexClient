using System;
using System.Collections.Generic;
using HexClientProject.Services.Providers;
using LcuApi;
using Newtonsoft.Json;

namespace HexClientProject.Services.Api
{
    public static class SocialApi
    {
        public static async System.Threading.Tasks.Task<string> GetFriends()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

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
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/friend-groups");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get list of friend groups - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return responseStr;
        }

        public static async System.Threading.Tasks.Task<string> GetFriendRequestsIn()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/friend-requests/");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get incoming friend request - Return code: " + response.StatusCode + " | " + responseStr);
            }

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(responseStr) ?? throw new InvalidOperationException();
            bool FilterCondition(dynamic x) => x.direction == "in";

            dynamic jsonResp = jsonObject.Filter((Func<dynamic, bool>)FilterCondition);

            return JsonConvert.SerializeObject(jsonResp);
        }

        public static async System.Threading.Tasks.Task<string> GetFriendRequestsOut()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/friend-requests/");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get outcoming friend request - Return code: " + response.StatusCode + " | " + responseStr);
            }

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(responseStr) ?? throw new InvalidOperationException();
            bool FilterCondition(dynamic x) => x.direction == "out";

            dynamic jsonResp = jsonObject.Filter((Func<dynamic, bool>)FilterCondition);

            return JsonConvert.SerializeObject(jsonResp);
        }

        public static async System.Threading.Tasks.Task<bool> SendFriendRequest(string gameName, string gameTag)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            var body = new { gameName = gameName, tagLine = gameTag };

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-chat/v2/friend-requests/", body);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot send friend request to" + gameName + "#" + gameTag + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        public static async System.Threading.Tasks.Task<bool> AcceptFriendRequest(string requestId)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Put, "lol-chat/v1/friend-requests/" + requestId);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot accept friend request: " + requestId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        public static async System.Threading.Tasks.Task<bool> RejectFriendRequest(string requestId)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-chat/v1/friend-requests/" + requestId);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot reject friend request: " + requestId + " - Return code: " + response.StatusCode + " | " + responseStr);
            }

            return true;
        }

        public static async System.Threading.Tasks.Task<bool> RemoveFriend(string summonerIdToRemove)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-chat/v1/friends/" + summonerIdToRemove);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot remove friend: " + summonerIdToRemove + " - Return code: " + response.StatusCode);
            }
            return true;
        }

        public static async System.Threading.Tasks.Task<bool> BlockPlayer(string summonerIdToBlock)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            var body = new { puuid = summonerIdToBlock };

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-chat/v1/blocked-players/", body);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot block player: " + summonerIdToBlock + " - Return code: " + response.StatusCode);
            }
            return true;
        }

        public static async System.Threading.Tasks.Task<bool> UnblockPlayer(string summonerIdToUnblock)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-chat/v1/blocked-players/" + summonerIdToUnblock);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot unblock player: " + summonerIdToUnblock + " - Return code: " + response.StatusCode);
            }
            return true;
        }

        public static async System.Threading.Tasks.Task<string> GetBlockedPlayer()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/blocked-players/");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get all blocked player: " + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
            return responseStr;
        }

        public static async System.Threading.Tasks.Task<bool> MutePlayer(string summonerIdToMute)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            var body = new { puuids = new List<string>(){ summonerIdToMute } };

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-chat/v1/player-mutes/", body);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot mute player: " + summonerIdToMute + " - Return code: " + response.StatusCode);
                return false;
            }
            return true;
        }

        //public static async System.Threading.Tasks.Task<bool> UnmutePlayer(string summonerIdToUnmute)
        //{
        //    ILeagueClient api = LcuWebSocketService.Instance().Result;

        //    System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-chat/v1/blocked-players/" + summonerIdToUnmute);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Err: Cannot unblock player: " + summonerIdToUnmute + " - Return code: " + response.StatusCode);
        //        return false;
        //    }
        //    return true;
        //}

        public static async System.Threading.Tasks.Task<string> GetMutedPlayer()
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-chat/v1/player-mutes/");
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot get muted player: " + " - Return code: " + response.StatusCode + " | " + responseStr);
            }
            return responseStr;
        }

        public static async void SendMessageToPlayer(string summonerId, string convId, string message)
        {
            ILeagueClient api = LcuWebSocketService.Instance().Result;

            var body = new { body = message };

            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-chat/v1/conversations/" + convId + "/messages", body);
            string responseStr = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot send message to player: " +summonerId+ " - Return code: " + response.StatusCode + " | " + responseStr);
            }
        }
    }
}
