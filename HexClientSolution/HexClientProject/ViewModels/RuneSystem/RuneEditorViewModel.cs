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
    public event Action? CloseRequested;

    private readonly RuneStateManager _runeStateManager = RuneStateManager.Instance;

    public ObservableCollection<RunePageModel> RunePages => _runeStateManager.RunePages;

    private RunePageModel _selectedPage;
    public RunePageModel SelectedPage
    {
        get => _selectedPage;
        set => this.RaiseAndSetIfChanged(ref _selectedPage, value);
    }

    private bool _isRenaming;
    public bool IsRenaming
    {
        get => _isRenaming;
        set => this.RaiseAndSetIfChanged(ref _isRenaming, value);
    }

    private string _renameText;
    public string RenameText
    {
        get => _renameText;
        set => this.RaiseAndSetIfChanged(ref _renameText, value);
    }

    public ReactiveCommand<Unit, Unit> StartRenamingCommand { get; }
    public ReactiveCommand<Unit, Unit> ConfirmRenameCommand { get; }
    public ReactiveCommand<Unit, Unit> AddPageCommand { get; }
    public ReactiveCommand<Unit, Unit> DeletePageCommand { get; }
    public ReactiveCommand<Unit, Unit> SavePageCommand { get; }
    public ReactiveCommand<Unit, Unit> CloseEditorCommand { get; }

    public RuneEditorViewModel()
    {
        _selectedPage = _runeStateManager.SelectedRunePage;
        _renameText = _selectedPage.PageName;

        StartRenamingCommand = ReactiveCommand.Create(() =>
        {
            RenameText = SelectedPage.PageName;
            IsRenaming = true;
        });

        ConfirmRenameCommand = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrWhiteSpace(RenameText)) return;
            ApiProvider.RuneService.RenameRunePage(SelectedPage.PageId, RenameText);
            ApiProvider.RuneService.LoadRunePages();
            IsRenaming = false;
        });

        AddPageCommand = ReactiveCommand.Create(() =>
        {
            ApiProvider.RuneService.CreateRunePage();
            ApiProvider.RuneService.LoadRunePages();
        });

        DeletePageCommand = ReactiveCommand.Create(() =>
        {
            ApiProvider.RuneService.DeleteRunePage(SelectedPage.PageId);
            ApiProvider.RuneService.LoadRunePages();
        });

        SavePageCommand = ReactiveCommand.Create(() =>
        {
            ApiProvider.RuneService.SaveCurrentRunePage();
        });

        CloseEditorCommand = ReactiveCommand.Create(() => CloseRequested?.Invoke());
    }
}