using System.Collections.Generic;
using System.Collections.ObjectModel;
using HexClientProject.Models.RuneSystem;
using ReactiveUI;

namespace HexClientProject.StateManagers;

public class RuneStateManager : ReactiveObject
{
    private static RuneStateManager? _instance;

    // Public property to access the single instance
    public static RuneStateManager Instance
    {
        get
        {
            var currInstance = _instance ??= new RuneStateManager();
            return currInstance;
        }
    }
    private ObservableCollection<RunePageModel> _runePages = new();
    public ObservableCollection<RunePageModel> RunePages
    {
        get => _runePages;
        set => this.RaiseAndSetIfChanged(ref _runePages, value);
    }

    private RunePageModel _selectedRunePage;
    public RunePageModel SelectedRunePage
    {
        get => _selectedRunePage;
        set => this.RaiseAndSetIfChanged(ref _selectedRunePage, value);
    }

    public int MaxPageCount;
    public int OwnedPageCount;
    public void AddRunePage(RunePageModel page)
    {
        RunePages.Add(page);
    }

    public void RemoveRunePage(RunePageModel page)
    {
        RunePages.Remove(page);
    }

    public void LoadRunePages(IEnumerable<RunePageModel> pages)
    {
        RunePages = new ObservableCollection<RunePageModel>(pages);
    }

    public void UpdatePageInventoryCounts(int maxPageCount, int ownedPageCount)
    {
        MaxPageCount = maxPageCount;
        OwnedPageCount = ownedPageCount;
    }
}