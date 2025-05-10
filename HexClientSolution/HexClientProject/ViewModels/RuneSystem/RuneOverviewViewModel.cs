using System;
using ReactiveUI;
using HexClientProject.Models.RuneSystem;
using HexClientProject.StateManagers;

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

    public RuneOverviewViewModel()
    {
        DisplayPage = DisplayableRunePageViewModel.Create(_runeStateManager.SelectedRunePage);
        Console.WriteLine(DisplayPage.PrimaryRunes.Count);
        Console.WriteLine(DisplayPage.SecondaryRunes.Count);
    }
}