using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using HexClientProject.Services.Providers;

namespace HexClientProject.Models.RuneSystem;

public static class RuneLookupTableModel
{
    public static ObservableCollection<RuneTreeModel> RuneTrees { get; set; } = [];
    
    private static Dictionary<int, RuneModel> _runeById = new();
    private static Dictionary<int, RuneTreeModel> _treeById = new();

    public static void FillLookupTables(List<RuneTreeModel> trees)
    {
        _treeById = trees.ToDictionary(t => t.Id);
        _runeById = trees
            .SelectMany(t => t.Slots.SelectMany(s => s.Runes))
            .ToDictionary(r => r.Id);
    }
    
    public static async Task Initialize()
    {
        // Fill up _runePages and _selectedRunePage fields in the RuneStateManager
        await ApiProvider.RuneService.LoadRunePages(); 
        var trees = await ApiProvider.RuneService.GetAllTrees(); 
        Dispatcher.UIThread.Post(() =>
        {
            FillLookupTables(trees);
            RuneTrees = new ObservableCollection<RuneTreeModel>(trees);
        });
    }

    public static RuneModel? GetRune(int id) => _runeById.GetValueOrDefault(id);

    public static RuneTreeModel? GetTree(int id) => _treeById.GetValueOrDefault(id);
}
