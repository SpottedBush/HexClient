using System.Collections.Generic;
using System.Linq;

namespace HexClientProject.Models.RuneSystem;

public static class DisplayableRunePageModel
{
    public record DisplayableRunePage(
        RuneTreeModel MainTree,
        RuneModel Keystone,
        List<RuneModel> PrimaryRunes,
        RuneTreeModel SecondaryTree,
        List<RuneModel> SecondaryRunes,
        List<RuneModel> StatMods
    );

    public static DisplayableRunePage ResolveRunePageModel(RunePageModel minimal)
    {
        var mainTree = RuneLookupTableModel.GetTree(minimal.MainTreeId) ?? throw new("Main tree not found");
        var secondaryTree = RuneLookupTableModel.GetTree(minimal.SecondaryTreeId) ?? throw new("Secondary tree not found");
        var keystone = RuneLookupTableModel.GetRune(minimal.KeystoneId) ?? throw new("Keystone not found");

        var primaryRunes = minimal.PrimaryRuneIds
            .Select(RuneLookupTableModel.GetRune)
            .Where(r => r is not null)
            .ToList()!;

        var secondaryRunes = minimal.SecondaryRuneIds
            .Select(RuneLookupTableModel.GetRune)
            .Where(r => r is not null)
            .ToList()!;

        var statPerks = minimal.StatModsIds
            .Select(RuneLookupTableModel.GetRune)
            .Where(r => r is not null)
            .ToList()!;

        return new DisplayableRunePage(mainTree, keystone, primaryRunes, secondaryTree, secondaryRunes, statPerks);
    }
}