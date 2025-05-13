using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

    public void SaveCurrentRunePage()
    {
    }

    public void SelectCurrentRunePage(int pageId)
    {
        throw new NotImplementedException();
    }

    public void DeleteRunePage(int pageId)
    {
        _runeStateManager.RunePages.RemoveAt(pageId);
    }

    public void RenameRunePage(int pageId, string newPageName)
    {
        throw new NotImplementedException();
    }

    public void GetPageInventory()
    {
        throw new NotImplementedException();
    }

    public async Task LoadRunePages()
    {
        RunePageModel customRunePageModel = await RunePageModel.LoadFromJsonAsync(
            new Uri("avares://HexClientProject/Assets/json/mocks/userRunePage1_mock.json"));
        _runeStateManager.RunePages =
        [
            customRunePageModel,
            customRunePageModel
        ];
        _runeStateManager.SelectedRunePage = _runeStateManager.RunePages[0];
    }
}