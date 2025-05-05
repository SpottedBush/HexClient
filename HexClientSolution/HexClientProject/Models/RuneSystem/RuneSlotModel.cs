namespace HexClientProject.Models.RuneSystem;

public class RuneSlotModel
{
    public int SlotIndex { get; set; }  // Row index
    public int SelectedRuneId { get; set; } // Column index
    public string IconPath { get; set; } = string.Empty;  // Path to the icon image.
}
