using System;
using System.Collections.ObjectModel;
using System.IO;
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
        RunePageModel emptyRunePage = new RunePageModel(1, "2Empty Page")
        {
            MainTreeId = 8100,
            SecondaryTreeId = 8300,
            KeystoneId = -1,
            PrimaryRuneIds = [-1, -1, -1],
            SecondaryRuneIds = [-1, -1, -1],
            StatModsIds = [-1, -1, -1]
        };
        _runeStateManager.SelectedRunePage = emptyRunePage;
        _runeStateManager.RunePages.Add(emptyRunePage);
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
        for (int i = _runeStateManager.RunePages.Count - 1; i >= 0; i--)
        {
            if (_runeStateManager.RunePages[i].PageId == pageId)
            {
                _runeStateManager.RunePages.RemoveAt(i);
            }
        }

        if (_runeStateManager.RunePages.Count == 0)
        {
            Console.WriteLine("No rune pages found");
            return;
        }
        _runeStateManager.SelectedRunePage = _runeStateManager.RunePages[0];
    }


    public void RenameRunePage(int pageId, string newPageName)
    {
        foreach (var runePage in _runeStateManager.RunePages)
        {
            if (runePage.PageId == pageId)
            {
                runePage.PageName = newPageName;
            }
        }
    }

    public void GetPageInventory()
    {
        _runeStateManager.MaxPageCount = 2;
        _runeStateManager.OwnedPageCount = _runeStateManager.RunePages.Count;
    }

    public async Task LoadRunePages()
    {
        if (_runeStateManager.RunePages.Count != 0) // Already initialized
        {
            _runeStateManager.RunePages = new ObservableCollection<RunePageModel>(_runeStateManager.RunePages);
            return;
        }
        Uri resourceUri = new Uri("avares://HexClientProject/Assets/json/mocks/userRunePage1_mock.json");
        var stream = AssetLoader.Open(resourceUri);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        RunePageModel customRunePageModel = JsonSerializer.Deserialize<RunePageModel>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidDataException("Failed to deserialize RunePageModel");
        RunePageModel emptyRunePage = new RunePageModel(1, "Empty Page")
        {
            MainTreeId = 8100, // Domination
            SecondaryTreeId = 8300, // Whimsy
            KeystoneId = -1,
            PrimaryRuneIds = [-1, -1, -1],
            SecondaryRuneIds = [-1, -1, -1],
            StatModsIds = [-1, -1, -1]
        };

        _runeStateManager.RunePages =
        [
            customRunePageModel,
            emptyRunePage
        ];
        _runeStateManager.SelectedRunePage = _runeStateManager.RunePages[0];
    }
}