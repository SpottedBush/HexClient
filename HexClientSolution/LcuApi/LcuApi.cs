using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace LcuApi;
public class LeagueClient : ILeagueClient
{
    private string ApiUri;

    private string LeaguePath;

    private int LeaguePid;

    private HttpClient httpClient;

    private RuneManager RuneManager;

    private Summoners Summoners;

    public string Token { get; set; }

    public ushort Port { get; set; }

    public event LeagueClosedHandler LeagueClosed;

    private LeagueClient()
    {
        httpClient = new HttpClient(new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (HttpRequestMessage httpRequestMessage, X509Certificate2 cert, X509Chain cetChain, SslPolicyErrors policyErrors) => true
        });
    }

    public static async Task<ILeagueClient> Connect()
    {
        Process process = await FindLeagueAsync();
        LeagueClient obj = (LeagueClient)(await Connect(Path.GetDirectoryName(process.MainModule.FileName)));
        obj.LeaguePid = process.Id;
        return obj;
    }

    public static async Task<ILeagueClient> Connect(string path)
    {
        LeagueClient ret = new LeagueClient
        {
            LeaguePath = path
        };
        await ret.WatchLockFileAsync();
        Process processById = Process.GetProcessById(ret.LeaguePid);
        processById.EnableRaisingEvents = true;
        processById.Exited += ret.League_Exited;
        return ret;
    }

    public async Task WatchLockFileAsync()
    {
        string lockFilePath = Path.Combine(LeaguePath, "lockfile");
        if (File.Exists(lockFilePath))
        {
            await ParseLockFileAsync(lockFilePath);
            return;
        }

        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        FileSystemWatcher watcher = new FileSystemWatcher(LeaguePath);
        FileSystemEventHandler handler = null;
        handler = async delegate (object s, FileSystemEventArgs e)
        {
            if (e.Name.Equals("lockfile"))
            {
                await ParseLockFileAsync(lockFilePath);
                tcs.TrySetResult(result: true);
                watcher.Created -= handler;
                watcher.Dispose();
            }
        };
        watcher.Created += handler;
        watcher.EnableRaisingEvents = true;
    }

    private async Task ParseLockFileAsync(string lockFile)
    {
        using FileStream fileStream = new FileStream(lockFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using StreamReader reader = new StreamReader(fileStream);
        string[] array = (await reader.ReadToEndAsync()).Split(new char[1] { ':' });
        Token = array[3];
        Port = ushort.Parse(array[2]);
        ApiUri = "https://127.0.0.1:" + Port + "/";
        Encoding.ASCII.GetBytes("riot:" + Token);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{Token}")));
        httpClient.BaseAddress = new Uri(ApiUri);
        if (LeaguePid == 0)
        {
            LeaguePid = int.Parse(array[1]);
        }
    }

    private static async Task<Process> FindLeagueAsync()
    {
        Process ret = null;
        await Task.Run(delegate
        {
            Process[] processesByName;
            while (true)
            {
                processesByName = Process.GetProcessesByName("LeagueClientUx");
                if (processesByName.Length != 0)
                {
                    break;
                }

                Thread.Sleep(1000);
            }

            ret = processesByName[0];
        });
        return ret;
    }

    private void League_Exited(object sender, EventArgs e)
    {
        this.LeagueClosed();
    }

    public RuneManager GetRuneManager()
    {
        if (RuneManager == null)
        {
            RuneManager = new RuneManager(this);
        }

        return RuneManager;
    }

    public HttpClient GetHttpClient()
    {
        return httpClient;
    }

    public async Task<HttpResponseMessage> MakeApiRequest(HttpMethod method, string endpoint, object data = null)
    {
        string content = ((data == null) ? "" : JsonConvert.SerializeObject(data));
        if (method == HttpMethod.Get)
        {
            return await httpClient.GetAsync(endpoint);
        }
        else if (method == HttpMethod.Post)
        {
            return await httpClient.PostAsync(endpoint, new StringContent(content, Encoding.UTF8, "application/json"));
        }
        else if (method == HttpMethod.Put)
        {
            return await httpClient.PutAsync(endpoint, new StringContent(content, Encoding.UTF8, "application/json"));
        }
        else if (method == HttpMethod.Delete)
        {
            return await httpClient.DeleteAsync(endpoint);
        }
        else
        {
            throw new Exception("Unsupported HTTP method");
        }
    }


    public async Task<T> MakeApiRequestAs<T>(HttpMethod method, string endpoint, object data = null)
    {
        return JsonConvert.DeserializeObject<T>(await (await MakeApiRequest(method, endpoint, data)).Content.ReadAsStringAsync());
    }

    public Summoners GetSummonersModule()
    {
        if (Summoners == null)
        {
            Summoners = new Summoners(this);
        }

        return Summoners;
    }
}