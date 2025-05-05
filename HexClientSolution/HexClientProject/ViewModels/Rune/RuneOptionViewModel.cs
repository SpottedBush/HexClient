using ReactiveUI;

namespace HexClientProject.ViewModels.Rune;

public class RuneOptionViewModel(int id, string name, string iconPath, string description)
    : ReactiveObject
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string IconPath { get; } = iconPath;
    public string Description { get; } = description;

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }
}