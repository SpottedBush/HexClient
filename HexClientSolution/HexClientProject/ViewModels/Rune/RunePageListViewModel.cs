using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using ReactiveUI;

namespace HexClientProject.ViewModels.Rune;

public class RunePageListViewModel : ReactiveObject
{
    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;
    private ObservableCollection<RunePageModel> Pages => _runeStateManager.RunePages;
    public ReactiveCommand<Unit, Unit> LoadCommand { get; }
    public ReactiveCommand<RunePageModel, Unit> DeleteCommand { get; }

    public RunePageListViewModel()
    {
        LoadCommand = ReactiveCommand.Create(ApiProvider.RuneService.LoadRunePages);
        DeleteCommand = ReactiveCommand.Create<RunePageModel>(DeletePage);
        ApiProvider.RuneService.LoadRunePages(); // Initial load
    }

    private void DeletePage(RunePageModel page)
    {
        ApiProvider.RuneService.DeleteRunePage(page.Id);
    }
}
