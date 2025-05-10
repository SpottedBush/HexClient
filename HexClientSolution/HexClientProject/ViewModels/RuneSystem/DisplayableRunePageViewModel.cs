using System.Collections.Generic;
using System.Linq;
using HexClientProject.Models.RuneSystem;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem;

public class DisplayableRunePageViewModel : ReactiveObject
{
    public RuneTreeViewModel MainTree { get; }
    public RuneTreeViewModel SecondaryTree { get; }
    public RuneViewModel Keystone { get; }
    public List<RuneViewModel> PrimaryRunes { get; }
    public List<RuneViewModel> SecondaryRunes { get; }
    public List<RuneViewModel> StatMods { get; }

    private DisplayableRunePageViewModel(
        RuneTreeViewModel mainTree,
        RuneTreeViewModel secondaryTree,
        RuneViewModel keystone,
        List<RuneViewModel> primaryRunes,
        List<RuneViewModel> secondaryRunes,
        List<RuneViewModel> statMods)
    {
        MainTree = mainTree;
        SecondaryTree = secondaryTree;
        Keystone = keystone;
        PrimaryRunes = primaryRunes;
        SecondaryRunes = secondaryRunes;
        StatMods = statMods;
    }
    
    public static DisplayableRunePageViewModel Create(RunePageModel model)
    {
        var mainTree = new RuneTreeViewModel(RuneLookupTableModel.GetTree(model.MainTreeId) ?? throw new("Main tree not found"));
        var secondaryTree = new RuneTreeViewModel(RuneLookupTableModel.GetTree(model.SecondaryTreeId) ?? throw new("Secondary tree not found"));
        var keystone = new RuneViewModel(RuneLookupTableModel.GetRune(model.KeystoneId) ?? throw new("Keystone not found"));

        var primary = model.PrimaryRuneIds
            .Select(RuneLookupTableModel.GetRune)
            .Where(r => r != null)
            .Select(r => new RuneViewModel(r!))
            .ToList();

        var secondary = model.SecondaryRuneIds
            .Select(RuneLookupTableModel.GetRune)
            .Where(r => r != null)
            .Select(r => new RuneViewModel(r!))
            .ToList();

        var statMods = model.StatModsIds
            .Select(RuneLookupTableModel.GetRune)
            .Where(r => r != null)
            .Select(r => new RuneViewModel(r!))
            .ToList();

        return new DisplayableRunePageViewModel(mainTree, secondaryTree, keystone, primary, secondary, statMods);
    }
}