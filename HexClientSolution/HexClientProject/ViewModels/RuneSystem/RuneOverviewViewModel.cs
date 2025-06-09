using System;
using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models.RuneSystem;
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
    public ObservableCollection<RunePageModel> RunePages => _runeStateManager.RunePages;
    private RunePageModel _selectedRunePage;
    public RunePageModel SelectedRunePage
    {
        get => _selectedRunePage;
        set => this.RaiseAndSetIfChanged(ref _selectedRunePage, value);
    }
    public ReactiveCommand<Unit, Unit> OpenEditorCommand { get; }
    public RuneOverviewViewModel(DraftViewModel parent)
    {
        OpenEditorCommand = ReactiveCommand.Create(parent.ShowEditorOverlay);
        ApiProvider.RuneService.CreateRunePage();

        _selectedRunePage = _runeStateManager.SelectedRunePage;
        SelectedRunePage = _runeStateManager.SelectedRunePage;

        // Keep the RuneStateManager's SelectedRunePage in sync
        this.WhenAnyValue(x => x.SelectedRunePage)
            .Subscribe(selected =>
            {
                DisplayPage = DisplayableRunePageViewModel.Create(selected);
                _runeStateManager.SelectedRunePage = selected;
            });
        _displayPage = DisplayableRunePageViewModel.Create(_runeStateManager.SelectedRunePage);
    }
}