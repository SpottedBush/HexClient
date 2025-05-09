using Avalonia.Media.Imaging;
using HexClientProject.Utils;

namespace HexClientProject.Models.RuneSystem;

public class RuneModel
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;
    public Bitmap Icon => PathUtils.PathToBitMap(IconPath);
    public string Name { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
}
