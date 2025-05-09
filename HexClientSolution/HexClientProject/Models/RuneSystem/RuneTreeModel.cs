using System;
using System.Collections.Generic;

namespace HexClientProject.Models.RuneSystem;

public class RuneTreeModel
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<RuneSlotModel> Slots { get; set; } = [];
}
