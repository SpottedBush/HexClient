using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HexClientProject.Interfaces;
using HexClientProject.Models.RuneSystem;
using HexClientProject.StateManagers;
using HexClientProject.Utils;

namespace HexClientProject.Services.Mocks;

public class RuneMock : IRuneService
{
    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;
    public Task<List<RuneTreeModel>> GetAllTrees()
    {
        return JsonLoaderUtils.LoadRuneTreesFromJsonAsync(StaticAssetPaths.RuneTreesJson); 
    }

    public void CreateRunePage()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateRunePage(int pageId)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteRunePage(int pageId)
    {
        throw new System.NotImplementedException();
    }

    public void RenameRunePage(int pageId, string newPageName)
    {
        throw new System.NotImplementedException();
    }

    public void GetPageInventory()
    {
        throw new System.NotImplementedException();
    }

    public async Task LoadRunePages()
    {
        try
        {
            RunePageModel customRunePageModel = await RunePageModel.LoadFromJsonAsync(new Uri("avares://HexClientProject/Assets/json/mocks/userRunePage1_mock.json"));
            _runeStateManager.RunePages =
            [
                customRunePageModel,
                customRunePageModel
            ];
            _runeStateManager.SelectedRunePage = _runeStateManager.RunePages[0];
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }

    public void SaveRunePages(IEnumerable<RunePageModel> pages)
    {
        throw new System.NotImplementedException();
    }
}