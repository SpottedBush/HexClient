using System;
using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem.RuneEditor;

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
    private RuneTreeViewModel? _primaryTree;
    public RuneTreeViewModel? PrimaryTree
    {
        get => _primaryTree;
        set => this.RaiseAndSetIfChanged(ref _primaryTree, value);
    }

    private SecondaryTreeViewModel? _secondaryTree;
    public SecondaryTreeViewModel? SecondaryTree
    {
        get => _secondaryTree;
        set => this.RaiseAndSetIfChanged(ref _secondaryTree, value);
    }

    private string _oldPageName = String.Empty;
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
        var mainTreeModel = RuneLookupTableModel.GetTree(SelectedPage.MainTreeId);
        if (mainTreeModel != null)
        {
            PrimaryTree = new RuneTreeViewModel(mainTreeModel);
        }
        this.WhenAnyValue(x => x.SelectedPage.MainTreeId)
            .Subscribe(id =>
            {
                var model = RuneLookupTableModel.GetTree(id);
                if (model != null)
                    PrimaryTree = new RuneTreeViewModel(model);
            });
        var secTreeModel = RuneLookupTableModel.GetTree(SelectedPage.SecondaryTreeId);
        if (secTreeModel != null)
        {
            SecondaryTree = new SecondaryTreeViewModel(secTreeModel);
        }
        this.WhenAnyValue(x => x.SelectedPage.SecondaryTreeId)
            .Subscribe(id =>
            {
                var model = RuneLookupTableModel.GetTree(id);
                if (model != null)
                    SecondaryTree = new SecondaryTreeViewModel(model);
            });
        
        
        StartRenamingCommand = ReactiveCommand.Create(() =>
        {
            _oldPageName = _selectedPage.PageName;
            if (IsRenaming)
            {
                ConfirmRenameCommand!.Execute().Subscribe();
                return;
            }
            IsRenaming = true;
        });
        
        CancelRenameCommand = ReactiveCommand.Create(() =>
        {
            SelectedPage.PageName = _oldPageName;
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
            SelectedPage = _runeStateManager.SelectedRunePage;
        });

        DeletePageCommand = ReactiveCommand.Create(() =>
        {
            IsRenaming = false;
            ApiProvider.RuneService.DeleteRunePage(SelectedPage.PageId);
            ApiProvider.RuneService.LoadRunePages();
            SelectedPage = _runeStateManager.SelectedRunePage;
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