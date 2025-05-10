using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexClientProject.Interfaces;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Services.Api;
using HexClientProject.StateManagers;
using Newtonsoft.Json;

namespace HexClientProject.Services.Builders;

public class RuneBuilder : IRuneService
{
    public Task<List<RuneTreeModel>> GetAllTrees()
    {
        throw new NotImplementedException();
    }

    public void CreateRunePage()
    {
        string response = RuneApi.AddPage().Result;
        if (string.IsNullOrEmpty(response))
        {
            throw new Exception("Failed to create rune page.");
        }
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

        // return jsonObject.id;
    }

    public void UpdateRunePage(int pageId)
    {
        List<int> selectedRunes = [];
        RunePageModel runePage = RuneStateManager.Instance.RunePages[pageId];
        selectedRunes = (List<int>)selectedRunes.Append(runePage.KeystoneId);
        foreach (int id in runePage.PrimaryRuneIds)
        {
            selectedRunes = (List<int>)selectedRunes.Append(id);
        }

        foreach (int id in runePage.SecondaryRuneIds)
        {
            selectedRunes = (List<int>)selectedRunes.Append(id);
        }

        foreach (int id in runePage.StatModsIds)
        {
            selectedRunes = (List<int>)selectedRunes.Append(id);
        }

        if (!RuneApi.UpdatePage(pageId, selectedRunes).Result)
        {
            throw new Exception("Failed to update rune page.");
        }

    }

    public void DeleteRunePage(int pageId)
    {
        if (!RuneApi.DeletePage(pageId).Result)
        {
            throw new Exception("Failed to delete rune page.");
        }
    }

    public void RenameRunePage(int pageId, string newPageName)
    {
        throw new NotImplementedException();
    }

    public void GetPageInventory()
    {
        string response = RuneApi.GetPagesInventory().Result;
        if (string.IsNullOrEmpty(response))
        {
            throw new Exception("Failed to get pages inventory.");
        }
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

        RuneStateManager.Instance.MaxPageCount = jsonObject.customPageCount;
        RuneStateManager.Instance.OwnedPageCount = jsonObject.ownedPageCount;
    }

    public Task LoadRunePages()
    {
        RuneStateManager.Instance.RunePages.Clear();

        string response = RuneApi.GetAllPages().Result;
        if (string.IsNullOrEmpty(response))
        {
            throw new Exception("Failed to get all pages.");
        }
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

        foreach (var r in jsonObject)
        {
            List<int> runeIds = r.selectedPerkIds;
            RuneStateManager.Instance.RunePages = RuneStateManager.Instance.RunePages.Append(new RunePageModel(runeIds));
        }

        return Task.CompletedTask;
    }

    public void SaveRunePages(IEnumerable<RunePageModel> pages)
    {
        throw new NotImplementedException();
    }
}