using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HexClientProject.Interfaces;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Services.Api;
using HexClientProject.StateManagers;
using Newtonsoft.Json;

namespace HexClientProject.Services.Builders;

public class RuneBuilder : IRuneService
{
    public void CreateRunePage()
    {
        string response = RuneApi.AddPage().Result;
        if (string.IsNullOrEmpty(response))
        {
            throw new Exception("Failed to create rune page.");
        }
        
        // Get id and name of the new page created
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

        RuneStateManager.Instance.RunePages.Add(new RunePageModel(jsonObject.id, jsonObject.name));
    }

    public void SaveCurrentRunePage()
    {
        List<int> selectedRunes = [];
        RunePageModel runePage = RuneStateManager.Instance.SelectedRunePage;
        selectedRunes.Add(runePage.KeystoneId);
        foreach (int id in runePage.PrimaryRuneIds.Concat(runePage.SecondaryRuneIds).Concat(runePage.StatModsIds))
        {
            selectedRunes.Add(id);
        }
        if (!RuneApi.UpdatePage(runePage.PageId, selectedRunes).Result)
        {
            throw new Exception("Failed to update rune page.");
        }

    }

    public void SelectCurrentRunePage(int pageId)
    {
        if (!RuneApi.SetCurrentPage(pageId).Result)
        {
            throw new Exception("Failed to set current rune page.");
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
        if (!RuneApi.RenamePage(pageId, newPageName).Result)
        {
            throw new Exception("Failed to rename rune page.");
        }
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
            RuneStateManager.Instance.RunePages.Add(new RunePageModel(runeIds));
        }

        return Task.CompletedTask;
    }
}