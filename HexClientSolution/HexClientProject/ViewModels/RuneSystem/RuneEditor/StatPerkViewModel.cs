using System.Text.RegularExpressions;
using Avalonia.Media.Imaging;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Utils;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem.RuneEditor;

public class StatPerkViewModel(RuneModel model) : ReactiveObject
{
    public int Id { get; } = model.Id;
    public Bitmap Icon { get; } = PathUtils.PathToBitMap(model.IconPath);
    public string ParsedLongDesc { get; } = Regex.Replace(
        model.ShortDescription.Replace("<br>", "\n").Replace("<br/>", "\n"), "<.*?>", ""
    );

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }
    public string Name => model.Name;
}