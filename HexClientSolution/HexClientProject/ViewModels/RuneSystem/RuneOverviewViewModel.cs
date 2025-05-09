using ReactiveUI;
using HexClientProject.Models.RuneSystem;
using HexClientProject.StateManagers;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneOverviewViewModel : ReactiveObject
{
    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;
    private DisplayableRunePageModel.DisplayableRunePage _displayPage;
    public DisplayableRunePageModel.DisplayableRunePage DisplayPage
    {
        get => _displayPage;
        set => this.RaiseAndSetIfChanged(ref _displayPage, value);
    }

    public RuneOverviewViewModel()
    {
        _displayPage = DisplayableRunePageModel.ResolveRunePageModel(_runeStateManager.SelectedRunePage);
        
    }
}