using System;
using System.Collections.Generic;

namespace HexClientProject.Models;

public class RunePageModel
{
    public string PageName { get; set; } = "New Page";

    public RuneTree PrimaryTree { get; set; }
    public RuneTree SecondaryTree { get; set; }
    public List<BonusStat> BonusStats { get; set; } = new();

    public RunePageModel Clone()
    {
        return new RunePageModel
        {
            PageName = PageName,
            PrimaryTree = PrimaryTree,
            SecondaryTree = SecondaryTree,
            BonusStats = [..BonusStats]
        };
    }
}

public class RuneTree
{
    public string TreeName { get; set; }
    public List<RuneModel> SelectedRunes { get; set; } = new();
}

public class RuneModel
{
    public int Id { get; set; }   // Riot Rune ID
    public string Name { get; set; }
    public string IconPath { get; set; }
    public string Description { get; set; }
}

public class BonusStat
{
    public string Type { get; set; }
    public string Value { get; set; }
}
