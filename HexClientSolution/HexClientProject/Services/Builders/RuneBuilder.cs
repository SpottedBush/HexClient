using System;
using System.Collections.Generic;
using System.Linq;
using HexClientProject.Interfaces;
using HexClientProject.Models;
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
            throw new System.Exception("Failed to create rune page.");
        }
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

        // return jsonObject.id;
    }

    public void UpdateRunePage(int pageId)
    {
        List<int> selectedRunes = [];
        RunePageModel runePage = RuneStateManager.Instance.RunePages[pageId];
        selectedRunes.Append(runePage.Keystone.Id);

        foreach (RuneModel r in runePage.PrimaryRunes)
        {
            selectedRunes.Append(r.Id);
        }

        foreach (RuneModel r in runePage.SecondaryRunes)
        {
            selectedRunes.Append(r.Id);
        }

        foreach (RuneModel r in runePage.StatPerks)
        {
            selectedRunes.Append(r.Id);
        }

        if (!RuneApi.UpdatePage(pageId, selectedRunes).Result)
        {
            throw new System.Exception("Failed to update rune page.");
        }

    }

    public void DeleteRunePage(int pageId)
    {
        if (!RuneApi.DeletePage(pageId).Result)
        {
            throw new System.Exception("Failed to delete rune page.");
        }
    }

    public void RenameRunePage(int pageId)
    {
        throw new System.NotImplementedException();
    }

    public void GetPageInventory()
    {
        string response = RuneApi.GetPagesInventory().Result;
        if (string.IsNullOrEmpty(response))
        {
            throw new System.Exception("Failed to get pages inventory.");
        }
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

        RuneStateManager.Instance.MaxPageCount = jsonObject.customPageCount;
        RuneStateManager.Instance.OwnedPageCount = jsonObject.ownedPageCount;
    }

    public void LoadRunePages()
    {
        RuneStateManager.Instance.RunePages.Clear();

        string response = RuneApi.GetAllPages().Result;
        if (string.IsNullOrEmpty(response))
        {
            throw new System.Exception("Failed to get all pages.");
        }
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

        foreach (var r in jsonObject)
        {
            List<int> runeIds = r.selectedPerkIds;
            RuneStateManager.Instance.RunePages.Append(new RunePageModel(runeIds));
        }
    }

    public void SaveRunePages(IEnumerable<RunePageModel> pages)
    {
        throw new System.NotImplementedException();
    }
}