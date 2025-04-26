namespace LcuApi;

public interface ILeagueClient
{
    event LeagueClosedHandler LeagueClosed;

    RuneManager GetRuneManager();

    Summoners GetSummonersModule();

    HttpClient GetHttpClient();

    Task<HttpResponseMessage> MakeApiRequest(HttpMethod method, string endpoint, object? data = null!);

    Task<T> MakeApiRequestAs<T>(HttpMethod method, string endpoint, object? data = null!);
}