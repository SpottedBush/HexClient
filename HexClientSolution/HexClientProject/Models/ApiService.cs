using System;
using LcuApi;

namespace HexClientProject.Models
{

    public class ApiService
    {
        public static async void CreateLobby(string gameId)
        {
            ILeagueClient api = await LeagueClient.Connect();
            var body = new { queueId = gameId };
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-lobby/v2/lobby/", body);
            String responseStr = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Err: Cannot create the lobby " + GameMode.GetGameModeFromGameId(gameId) + " - Return code: " + response.StatusCode);
            }
        }

        public async void CreateCustomLobby()
        {

        }
    }
    

}
