using System.Net;
using Newtonsoft.Json;

namespace LcuApi;

public class RuneManager(ILeagueClient league)
{
    private const string EndpointRoot = "lol-perks/v1/";

    public RunePage GetCurrentRunePage()
    {
        return league.MakeApiRequestAs<RunePage>(HttpMethod.Get, "lol-perks/v1/currentpage").Result;
    }

    public RunePage[] GetRunePages()
    {
        return league.MakeApiRequestAs<RunePage[]>(HttpMethod.Get, "lol-perks/v1/pages").Result;
    }

    public RunePage GetRunePageById(int id)
    {
        return league.MakeApiRequestAs<RunePage>(HttpMethod.Get, "lol-perks/v1/page/" + id).Result;
    }

    public int GetOwnedPageCount()
    {
        return int.Parse(league.MakeApiRequest(HttpMethod.Get, "lol-perks/v1/inventory").Result.Content.ReadAsStringAsync().Result);
    }

    private bool DeleteRunePage(int id)
    {
        return league.MakeApiRequest(HttpMethod.Delete, "lol-perks/v1/page/" + id).Result.StatusCode == HttpStatusCode.OK;
    }

    public AddRuneResult AddRunePage(RunePage? page)
    {
        HttpResponseMessage result = league.MakeApiRequest(HttpMethod.Post, "lol-perks/v1/pages", page).Result;
        if (result.StatusCode != HttpStatusCode.OK && JsonConvert.DeserializeObject<Error>(result.Content.ReadAsStringAsync().Result)!.Message.Equals("Max pages reached"))
        {
            return AddRuneResult.MaxPageReached;
        }

        return AddRuneResult.Success;
    }
}