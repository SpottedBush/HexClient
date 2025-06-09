using System;
using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneEditorViewModel : ReactiveObject
{

    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;

    public ObservableCollection<RunePageModel> RunePages => _runeStateManager.RunePages;

    private RunePageModel _selectedPage;
    public RunePageModel SelectedPage
    {
        get => _selectedPage;
        set => this.RaiseAndSetIfChanged(ref _selectedPage, value);
    }

    private string oldPageName = null;
    private bool _isRenaming;
    public bool IsRenaming
    {
        get => _isRenaming;
        set => this.RaiseAndSetIfChanged(ref _isRenaming, value);
    }
    
    public ReactiveCommand<Unit, Unit> StartRenamingCommand { get; }
    public ReactiveCommand<Unit, Unit> ConfirmRenameCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelRenameCommand { get; }
    public ReactiveCommand<Unit, Unit> AddPageCommand { get; }
    public ReactiveCommand<Unit, Unit> DeletePageCommand { get; }
    public ReactiveCommand<Unit, Unit> SavePageCommand { get; }
    public event Action? CloseEditorRequested;
    public ReactiveCommand<Unit, Unit> CloseEditorCommand { get; }

    public RuneEditorViewModel()
    {
        _selectedPage = _runeStateManager.SelectedRunePage;
        StartRenamingCommand = ReactiveCommand.Create(() =>
        {
            oldPageName = _selectedPage.PageName;
            if (IsRenaming)
            {
                ConfirmRenameCommand!.Execute().Subscribe();
                return;
            }
            IsRenaming = true;
        });
        
        CancelRenameCommand = ReactiveCommand.Create(() =>
        {
            SelectedPage.PageName = oldPageName;
            IsRenaming = false;
        });
        
        ConfirmRenameCommand = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrWhiteSpace(SelectedPage.PageName)) return;
            ApiProvider.RuneService.RenameRunePage(SelectedPage.PageId, SelectedPage.PageName);
            ApiProvider.RuneService.LoadRunePages();
            IsRenaming = false;
        });

        AddPageCommand = ReactiveCommand.Create(() =>
        {
            IsRenaming = false;
            ApiProvider.RuneService.CreateRunePage();
            ApiProvider.RuneService.LoadRunePages();
        });

        DeletePageCommand = ReactiveCommand.Create(() =>
        {
            IsRenaming = false;
            ApiProvider.RuneService.DeleteRunePage(SelectedPage.PageId);
            ApiProvider.RuneService.LoadRunePages();
        });

        SavePageCommand = ReactiveCommand.Create(() =>
        {
            IsRenaming = false;
            ApiProvider.RuneService.SaveCurrentRunePage();
            ApiProvider.RuneService.LoadRunePages();
        });

        CloseEditorCommand = ReactiveCommand.Create(() =>
        {
            IsRenaming = false;
            CloseEditorRequested?.Invoke();
        });
    }
}