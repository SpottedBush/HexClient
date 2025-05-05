using System;

namespace HexClientProject.Models.RuneSystem;

public class RunePageModel
{
    public int FormatVersion { get; set; } = 4;
    public bool IsCurrentPage { get; set; }
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeletable { get; set; } = true;
    public bool IsEditable { get; set; } = true;
    public bool IsValid { get; set; } = true;
    public string Name { get; set; } = String.Empty;
    public RuneTreeModel PrimaryTree { get; set; }
    public RuneTreeModel SecondaryTree { get; set; }
}