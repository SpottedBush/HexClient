using HexClientProject.Services.Providers;
using LcuApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HexClientProject.Services.Api;

public class RuneApi
{
    public static async Task<string> GetAllPages()
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-perks/v1/pages");
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot get all pages: " + " - Return code: " + response.StatusCode + " | " + responseStr);
        }
        return responseStr;
    }

    public static async Task<string> GetPageById(int pageId)
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-perks/v1/pages/" + pageId.ToString());
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot get page: " +pageId.ToString()+ " - Return code: " + response.StatusCode + " | " + responseStr);
        }
        return responseStr;
    }

    public static async Task<string> GetCurrentPage()
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-perks/v1/currentpage");
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot get current page: " + " - Return code: " + response.StatusCode + " | " + responseStr);
        }
        return responseStr;
    }

    public static async Task<bool> SetCurrentPage(int pageId)
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;

        var body = new { pageId };

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Put, "lol-perks/v1/currentpage", body);
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot set current page: " + " - Return code: " + response.StatusCode + " | " + responseStr);
        }
        return response.IsSuccessStatusCode;
    }

    public static async Task<string> AddPage()
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Post, "lol-perks/v1/pages");
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot add page: " + " - Return code: " + response.StatusCode + " | " + responseStr);
        }

        return responseStr;
    }

    public static async Task<bool> UpdatePage(int pageId, List<int> selectedRunesId)
    {
        if (selectedRunesId.Count != 9)
        {
            return false;
        } 

        ILeagueClient api = LcuWebSocketService.Instance().Result;

        var body = new { id = pageId, selectedPerkIds = selectedRunesId };

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Put, "lol-perks/v1/pages/" + pageId.ToString(), body);
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot update page: " + pageId.ToString() + " - Return code: " + response.StatusCode + " | " + responseStr);
        }

        return response.IsSuccessStatusCode;
    }

    public static async Task<bool> DeletePage(int pageId)
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-perks/v1/pages/" + pageId.ToString());
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot delete page: " + pageId.ToString() + " - Return code: " + response.StatusCode + " | " + responseStr);
        }

        return response.IsSuccessStatusCode;
    }

    public static async Task<bool> DeleteAllPages()
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Delete, "lol-perks/v1/pages/");
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot delete all pages: " + " - Return code: " + response.StatusCode + " | " + responseStr);
        }

        return response.IsSuccessStatusCode;
    }

    public static async Task<bool> RenamePage(int pageId, string newName)
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;
        
        var body = new { id = pageId, name = newName };

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Put, "lol-perks/v1/pages/validate", body);
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot rename page: " + pageId.ToString() + " - Return code: " + response.StatusCode + " | " + responseStr);
        }

        return response.IsSuccessStatusCode;
    }

    public static async Task<string> GetPagesInventory()
    {
        ILeagueClient api = LcuWebSocketService.Instance().Result;

        System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-perks/v1/inventory");
        string responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Err: Cannot rename page: " + " - Return code: " + response.StatusCode + " | " + responseStr);
        }

        return responseStr;
    }


}