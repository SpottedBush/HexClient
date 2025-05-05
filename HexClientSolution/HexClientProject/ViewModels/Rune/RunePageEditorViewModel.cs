using System.Linq;
using System.Reactive;
using HexClientProject.Interfaces;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using ReactiveUI;

namespace HexClientProject.ViewModels.Rune;

public class RunePageEditorViewModel : ReactiveObject
{
    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;
    private RunePageModel RunePage { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public RunePageEditorViewModel(RunePageModel page)
    {
        RunePage = page;

        SaveCommand = ReactiveCommand.Create(SaveCurrentPage);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    private void SaveCurrentPage()
    {
        ApiProvider.RuneService.UpdateRunePage(RunePage.Id);
    }

    private void Cancel()
    {
        if (_runeStateManager.RunePages.Count == 0)
            return;
        // Reset the changes if needed
        RunePageModel original = _runeStateManager.RunePages.FirstOrDefault(p => p.Id == RunePage.Id)!;
        RunePage.Name = original.Name;
        RunePage.PrimaryTree = original.PrimaryTree;
        RunePage.SecondaryTree = original.SecondaryTree;
    }
}
