using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Platform;
using HexClientProject.Interfaces;
using HexClientProject.Models.RuneSystem;
using HexClientProject.StateManagers;

namespace HexClientProject.Services.Mocks;

public class RuneMock : IRuneService
{
    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;

    public void CreateRunePage()
    {
        RunePageModel customRunePageModel = new RunePageModel();
        _runeStateManager.SelectedRunePage = customRunePageModel;
        _runeStateManager.RunePages = (ObservableCollection<RunePageModel>)_runeStateManager.RunePages.Append(customRunePageModel);
    }

    // There is nowhere to save the rune page to. Thus doing nothing, could eventually save it under JSON format locally
    public void SaveCurrentRunePage()
    {
    }

    public void SelectCurrentRunePage(int pageId)
    {
        foreach (var runePage in _runeStateManager.RunePages)
        {
            if (runePage.PageId == pageId)
            {
                _runeStateManager.SelectedRunePage = runePage;
            }
        }
    }

    public void DeleteRunePage(int pageId)
    {
        foreach (var runePage in _runeStateManager.RunePages)
        {
            if (runePage.PageId == pageId)
            {
                _runeStateManager.RunePages.Remove(runePage);
            }
        }
    }

    public void RenameRunePage(int pageId, string newPageName)
    {
        foreach (var runePage in _runeStateManager.RunePages)
        {
            if (runePage.PageId == pageId)
                runePage.PageName = newPageName;
        }
    }

    public void GetPageInventory()
    {
        _runeStateManager.MaxPageCount = 2;
        _runeStateManager.OwnedPageCount = _runeStateManager.RunePages.Count;
    }

    public async Task LoadRunePages()
    {
        Uri resourceUri = new Uri("avares://HexClientProject/Assets/json/mocks/userRunePage1_mock.json");
        var stream = AssetLoader.Open(resourceUri);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        RunePageModel customRunePageModel = JsonSerializer.Deserialize<RunePageModel>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidDataException("Failed to deserialize RunePageModel");
        _runeStateManager.RunePages =
        [
            customRunePageModel,
            customRunePageModel
        ];
        _runeStateManager.SelectedRunePage = _runeStateManager.RunePages[0];
    }
}