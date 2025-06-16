using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Threading;
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

    public ObservableCollection<RuneTreeViewModel> AvailableTrees { get; } = new();

    
    private RuneTreeViewModel? _primaryTree;
    public RuneTreeViewModel? PrimaryTree
    {
        get => _primaryTree;
        private set => this.RaiseAndSetIfChanged(ref _primaryTree, value);
    }

    private void InitializeAvailableTrees()
    {
        AvailableTrees.Clear();
        foreach (var model in RuneLookupTableModel.RuneTrees)
        {
            var vm = new RuneTreeViewModel(model);

            // Monitor selection change
            vm.WhenAnyValue(x => x.IsPrimarySelected)
                .Where(x => x)
                .Subscribe(_ => OnSelectedTree(mainTree:vm));
            vm.WhenAnyValue(x => x.IsSecondarySelected)
                .Where(x => x)
                .Subscribe(_ => OnSelectedTree(secondaryTree:vm));
            AvailableTrees.Add(vm);
        }

        // Pre-select PrimaryTree if already set
        if (PrimaryTree != null)
        {
            var selected = AvailableTrees.FirstOrDefault(t => t.Id == PrimaryTree.Id);
            if (selected != null)
                selected.IsPrimarySelected = true;
        }
        // Or select a default tree
        else
        {
            var defaultTree = AvailableTrees.FirstOrDefault();
            if (defaultTree != null)
                defaultTree.IsPrimarySelected = true;
        }
        return;

        void OnSelectedTree(RuneTreeViewModel? mainTree = null, RuneTreeViewModel? secondaryTree = null)
        {
            if (mainTree != null)
            {
                if (PrimaryTree != null && PrimaryTree.Id == mainTree.Id)
                {
                    return;
                }
                // Clean runes in the previous tree
                DeselectAllRunes(needMainDeselect:true, needSecondaryDeselect:false);
                foreach (var tree in AvailableTrees)
                    tree.IsPrimarySelected = tree == mainTree;

                PrimaryTree = mainTree;
                AvailableSecondaryTrees.Clear();
                foreach (var tree in AvailableTrees)
                {
                    if (!tree.IsPrimarySelected)
                    {
                        AvailableSecondaryTrees.Add(tree);
                    }
                }

                if (SecondaryTree != null && mainTree.Name == SecondaryTree.Name)
                {
                    // Restore the same tree from updated AvailableSecondaryTrees
                    var newSec = AvailableSecondaryTrees.FirstOrDefault();
                    if (newSec == null) return;
                    foreach (var tree in AvailableSecondaryTrees)
                    {
                        if (tree != newSec)
                            tree.IsSecondarySelected = false;
                    }
                    SelectSecTree(newSec.Model);
                    SecondaryTree = new SecondaryTreeViewModel(newSec);
                }
                else if (SecondaryTree != null)
                {
                    var oldSec = AvailableSecondaryTrees.FirstOrDefault(t => t.Id == SecondaryTree.TreeId);
                    if (oldSec == null) return;
                    foreach (var tree in AvailableSecondaryTrees)
                    {
                        if (tree != oldSec)
                            tree.IsSecondarySelected = false;
                    }
                    SecondaryTree = new SecondaryTreeViewModel(oldSec);
                }
            }
            else if (secondaryTree != null)
            {
                if (PrimaryTree != null && PrimaryTree.Id == secondaryTree.Id)
                {
                    return;
                }
                // Clean runes in the previous tree
                DeselectAllRunes(needMainDeselect:false, needSecondaryDeselect:true);
                foreach (var tree in AvailableSecondaryTrees)
                {
                    if (tree != secondaryTree)
                        tree.IsSecondarySelected = false;
                }
                SecondaryTree = new SecondaryTreeViewModel(secondaryTree);
            }
        }
    }

    private void DeselectAllRunes(bool needMainDeselect = true, bool needSecondaryDeselect = true)
    {
        if (PrimaryTree != null && needMainDeselect)
        {
            foreach (var slot in PrimaryTree!.Slots)
            {
                slot.SelectedRune = null;
                foreach (var rune in slot.Runes)
                {
                    rune.IsSelected = false;
                }
            }
        }

        if (SecondaryTree == null || !needSecondaryDeselect) return;
        foreach (var slot in SecondaryTree.Slots)
        {
            slot.SelectedRune = null;
            foreach (var rune in slot.Runes)
            {
                rune.IsSelected = false;
            }
        }
    }

    public void SelectRunesFromPage(RunePageModel page)
    {
        // Updating Main Tree
        RuneTreeModel treeModel = RuneLookupTableModel.GetTree(page.MainTreeId)!;
        SelectMainTree(treeModel);
        // Updating Secondary Tree
        RuneTreeModel secTree = RuneLookupTableModel.GetTree(page.SecondaryTreeId)!;
        SelectSecTree(secTree);
        foreach (var tree in AvailableSecondaryTrees)
        {
            if (tree.Id == page.SecondaryTreeId)
                SecondaryTree = new SecondaryTreeViewModel(tree);
        }
        DeselectAllRunes();
        // Updating primary keystone and runes
        PrimaryTree!.Slots[0].SelectedRune = new RuneViewModel(RuneLookupTableModel.GetRune(page.KeystoneId)!);
        for (int i = 0; i < PrimaryTree.Slots.Count; i++)
        {
            foreach (var rune in PrimaryTree.Slots[i].Runes)
            {
                rune.IsSelected = (i !=0 && rune.Id == page.PrimaryRuneIds[i - 1]) || rune.Id == page.KeystoneId;
            }
        }
        
        // Updating secondary runes
        Dispatcher.UIThread.Post(() => {
            for (int i = 0; i < SecondaryTree!.Slots.Count; i++)
            {
                foreach (var rune in SecondaryTree.Slots[i].Runes)
                {
                    rune.IsSelected = page.SecondaryRuneIds.Contains(rune.Id);
                }
            }
        });
        
        // Updates statMods
        for (int i = 0; i < StatPerksViewModel.Rows.Count; i++)
        {
            var currentRow = StatPerksViewModel.Rows[i];
            foreach (var statMod in currentRow)
            {
                statMod.IsSelected = page.StatModsIds[i] == statMod.Id;
            }
        }

        SelectedPage = page;
    }
    
    private void SelectMainTree(RuneTreeModel mainTree)
    {
        var treeVm = AvailableTrees.FirstOrDefault(t => t.Id == mainTree.Id);
        if (PrimaryTree != null && PrimaryTree.Id == mainTree.Id || treeVm == null)
        {
            return;
        }
        // Clean runes in the previous tree
        DeselectAllRunes(needMainDeselect:true,  needSecondaryDeselect:false);
        foreach (var tree in AvailableTrees)
            tree.IsPrimarySelected = tree == treeVm;

        PrimaryTree = treeVm;
        
        AvailableSecondaryTrees.Clear();
        foreach (var tree in AvailableTrees)
        {
            if (!tree.IsPrimarySelected)
            {
                AvailableSecondaryTrees.Add(tree);
            }
        }
    }

    private void SelectSecTree(RuneTreeModel? secTree)
    {
        if (secTree == null) return;
        var treeVm = AvailableSecondaryTrees.FirstOrDefault(t => t.Id == secTree.Id);
        if (treeVm == null) return;
        
        foreach (var tree in AvailableSecondaryTrees)
        {
            tree.IsSecondarySelected = tree == treeVm;
        }
        DeselectAllRunes(needMainDeselect:false, needSecondaryDeselect:true);
        SecondaryTree = new SecondaryTreeViewModel(treeVm);
    }
    
    
    private ObservableCollection<RuneTreeViewModel> _availableSecondaryTrees = [];
    public ObservableCollection<RuneTreeViewModel> AvailableSecondaryTrees
    {
        get => _availableSecondaryTrees;
        private set => this.RaiseAndSetIfChanged(ref _availableSecondaryTrees, value);
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
    public StatPerksViewModel StatPerksViewModel { get; }
    public RuneEditorViewModel()
    {
        InitializeAvailableTrees();
        
        StatPerksViewModel = new StatPerksViewModel();
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
        SelectRunesFromPage(_runeStateManager.SelectedRunePage);
        
        
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