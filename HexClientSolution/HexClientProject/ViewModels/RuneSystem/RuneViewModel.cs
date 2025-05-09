using HexClientProject.Models.RuneSystem;
using HexClientProject.Utils;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneViewModel : ReactiveObject
{
    public RuneModel Model { get; }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }

    public RuneViewModel(RuneModel model)
    {
        Model = model;
    }

    public int Id => Model.Id;
    public string Name => Model.Name;
    public Avalonia.Media.Imaging.Bitmap Icon => PathUtils.PathToBitMap(Model.IconPath);
    public string LongDescription => Model.LongDescription;
    public string ShortDescription => Model.ShortDescription;
}