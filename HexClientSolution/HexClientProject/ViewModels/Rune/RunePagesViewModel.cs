using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using LcuApi;
using ReactiveUI;

namespace HexClientProject.ViewModels.Rune;

public class RunePagesViewModel : ReactiveObject
{
    public ReactiveCommand<Unit, Unit> AddNewPageCommand { get; }
    public ReactiveCommand<RunePageModel, Unit> DeletePageCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveAllCommand { get; }
    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;
    private ObservableCollection<RunePageModel> RunePages => _runeStateManager.RunePages;

    public RunePagesViewModel()
    {
        AddNewPageCommand = ReactiveCommand.Create(AddNewPage);
        DeletePageCommand = ReactiveCommand.Create<RunePageModel>(DeletePage);
        SaveAllCommand = ReactiveCommand.Create(SaveAll);
    }
    
    private void AddNewPage()
    {
        ApiProvider.RuneService.CreateRunePageModel();
        ApiProvider.RuneService.LoadRunePages();
    }

    private void DeletePage(RunePageModel runePageModel)
    {
        // Should delete RuneStateManager.Instance.SelectedRunePage
        ApiProvider.RuneService.DeleteRunePageModel();
        ApiProvider.RuneService.LoadRunePages();
    }

    private void SaveAll()
    {
        ApiProvider.RuneService.SaveRunePages(RunePages);
    }
}