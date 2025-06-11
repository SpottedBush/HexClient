using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HexClientProject.Models.RuneSystem;

public class RuneTreeModel
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    [JsonPropertyName("icon")]
    public string IconPath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsSelected { get; set; } = false;
    public List<RuneSlotModel> Slots { get; set; } = [];
}
