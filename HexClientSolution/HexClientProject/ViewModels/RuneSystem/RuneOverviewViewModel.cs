using System.Reactive;
using HexClientProject.Services.Providers;
using ReactiveUI;
using HexClientProject.StateManagers;
using HexClientProject.ViewModels.DraftPhase;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneOverviewViewModel : ReactiveObject
{
    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;
    private DisplayableRunePageViewModel _displayPage;
    public DisplayableRunePageViewModel DisplayPage
    {
        get => _displayPage;
        set => this.RaiseAndSetIfChanged(ref _displayPage, value);
    }
    
    public ReactiveCommand<Unit, Unit> OpenEditorCommand { get; }
    public RuneOverviewViewModel(DraftViewModel parent)
    {
        OpenEditorCommand = ReactiveCommand.Create(() => parent.ShowEditorOverlay());
        _displayPage = DisplayableRunePageViewModel.Create(_runeStateManager.SelectedRunePage);
    }
}