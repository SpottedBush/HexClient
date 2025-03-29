using LcuApi.DataObjects;

namespace LcuApi;

public class Summoners
{
    private ILeagueClient League;

    private const string endpointRoot = "/lol-summoner/v1/";

    public Summoners(ILeagueClient league)
    {
        League = league;
    }

    public bool IsNameAvailable(string name)
    {
        return bool.Parse(League.MakeApiRequest(HttpMethod.Get, "/lol-summoner/v1/check-name-availability/" + name).Result.Content.ReadAsStringAsync().Result);
    }

    public SummonerProfile GetCurrentSummoner()
    {
        return League.MakeApiRequestAs<SummonerProfile>(HttpMethod.Get, "/lol-summoner/v1/current-summoner").Result;
    }

    public SummonerProfile GetSummonerProfile(string name)
    {
        return League.MakeApiRequestAs<SummonerProfile>(HttpMethod.Get, "/lol-summoner/v1/summoners/" + name).Result;
    }
}