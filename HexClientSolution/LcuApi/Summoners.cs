namespace LcuApi;

public class Summoners(ILeagueClient league)
{
    public bool IsNameAvailable(string name)
    {
        return bool.Parse(league.MakeApiRequest(HttpMethod.Get, "/lol-summoner/v1/check-name-availability/" + name).Result.Content.ReadAsStringAsync().Result);
    }

    public SummonerProfile GetCurrentSummoner()
    {
        return league.MakeApiRequestAs<SummonerProfile>(HttpMethod.Get, "/lol-summoner/v1/current-summoner").Result;
    }

    public SummonerProfile GetSummonerProfile(string name)
    {
        return league.MakeApiRequestAs<SummonerProfile>(HttpMethod.Get, "/lol-summoner/v1/summoners/" + name).Result;
    }
}