using System;
using System.Text.RegularExpressions;
using Avalonia.Media.Imaging;
using HexClientProject.Models.RuneSystem;
using HexClientProject.Utils;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneViewModel(RuneModel model) : ReactiveObject
{
    private RuneModel Model { get; } = model;

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }

    public int Id => Model.Id;
    public string Name => Model.Name;
    public Bitmap Icon => PathUtils.PathToBitMap(Model.IconPath);
    public string LongDescription => Model.LongDescription;
    public string ShortDescription => Model.ShortDescription;
    
    public string ParsedLongDesc => ConvertHtmlToFormattedText(LongDescription);

    private string ConvertHtmlToFormattedText(string html)
    {
        return Regex.Replace(html
            .Replace("<br>", "\n")
            .Replace("<br/>", "\n"), "<.*?>","");
    }
}