using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using HexClientProject.Services.Providers;
using HexClientProject.Utils;

namespace HexClientProject.Models.RuneSystem;

public static class RuneLookupTableModel
{
    public static ObservableCollection<RuneTreeModel> RuneTrees { get; set; } = [];
    private static Dictionary<int, RuneModel> _statsModsById = new();
    private static Dictionary<int, RuneModel> _runeById = new();
    private static Dictionary<int, RuneTreeModel> _treeById = new();

    private static void FillTreeLookupTables(List<RuneTreeModel> trees)
    {
        _treeById = trees.ToDictionary(t => t.Id);
        _runeById = trees
            .SelectMany(t => t.Slots.SelectMany(s => s.Runes))
            .ToDictionary(r => r.Id);
    }

    private static void FillStatModsLookupTables(List<RuneModel> statMods)
    {
        _statsModsById = statMods.ToDictionary(t => t.Id);
    }
    
    public static async Task Initialize()
    {
        // Fill up _runePages and _selectedRunePage fields in the RuneStateManager
        await ApiProvider.RuneService.LoadRunePages();
        var trees = await JsonLoaderUtils.LoadRuneTreesFromJsonAsync(StaticAssetPaths.RuneTreesJson);
        
        var statMods = await JsonLoaderUtils.LoadStatModsFromJsonAsync(StaticAssetPaths.StatModsJson);
        Dispatcher.UIThread.Post(() =>
        {
            FillTreeLookupTables(trees);
            FillStatModsLookupTables(statMods);
            RuneTrees = new ObservableCollection<RuneTreeModel>(trees);
        });
        
    }

    public static RuneModel? GetRune(int id) => _runeById.GetValueOrDefault(id);

    public static RuneTreeModel? GetTree(int id) => _treeById.GetValueOrDefault(id);
    
    public static RuneModel? GetStatMod(int id) => _statsModsById.GetValueOrDefault(id);
}