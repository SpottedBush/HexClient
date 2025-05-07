using System.Collections.Generic;
using Avalonia.Media.Imaging;

namespace HexClientProject.Models.RuneSystem;

public class RunePageModel
{
    public string PrimaryTreeName { get; set; }
    public string SecondaryTreeName { get; set; }

    public RuneModel Keystone { get; set; }
    public List<RuneModel> PrimaryRunes { get; set; } = new();
    public List<RuneModel> SecondaryRunes { get; set; } = new();
    public List<RuneModel> StatPerks { get; set; } = new();
}

public class RuneModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Bitmap? IconPath { get; set; }
}
