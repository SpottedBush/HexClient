using System.Net;
using Newtonsoft.Json;

namespace LcuApi;

public class RuneManager
{
    private ILeagueClient League;

    private const string endpointRoot = "lol-perks/v1/";

    public RuneManager(ILeagueClient league)
    {
        League = league;
    }

    public RunePage GetCurrentRunePage()
    {
        return League.MakeApiRequestAs<RunePage>(HttpMethod.Get, "lol-perks/v1/currentpage").Result;
    }

    public RunePage[] GetRunePages()
    {
        return League.MakeApiRequestAs<RunePage[]>(HttpMethod.Get, "lol-perks/v1/pages").Result;
    }

    public RunePage GetRunePageById(int id)
    {
        return League.MakeApiRequestAs<RunePage>(HttpMethod.Get, "lol-perks/v1/page/" + id).Result;
    }

    public int GetOwnedPageCount()
    {
        return int.Parse(League.MakeApiRequest(HttpMethod.Get, "lol-perks/v1/inventory").Result.Content.ReadAsStringAsync().Result);
    }

    private bool DeleteRunePage(int id)
    {
        return League.MakeApiRequest(HttpMethod.Delete, "lol-perks/v1/page/" + id).Result.StatusCode == HttpStatusCode.OK;
    }

    public AddRuneResult AddRunePage(RunePage? page)
    {
        HttpResponseMessage result = League.MakeApiRequest(HttpMethod.Post, "lol-perks/v1/pages", page).Result;
        if (result.StatusCode != HttpStatusCode.OK && JsonConvert.DeserializeObject<Error>(result.Content.ReadAsStringAsync().Result).Message.Equals("Max pages reached"))
        {
            return AddRuneResult.MaxPageReached;
        }

        return AddRuneResult.Success;
    }
}