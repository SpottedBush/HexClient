using System.Collections.ObjectModel;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Services.Providers;
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
            _instance ??= new RuneStateManager();
            return _instance;
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
}