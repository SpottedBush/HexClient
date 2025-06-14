using HexClientProject.Models.RuneSystem;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media.Imaging;
using HexClientProject.Utils;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneTreeViewModel : ReactiveObject
{
    public RuneTreeModel Model { get; }
    public int Id => Model.Id;
    public string Name => Model.Name;
    
    private bool _isPrimarySelected;
    public bool IsPrimarySelected
    {
        get => _isPrimarySelected;
        set => this.RaiseAndSetIfChanged(ref _isPrimarySelected, value);
    }
    private bool _isSecondarySelected;
    public bool IsSecondarySelected
    {
        get => _isSecondarySelected;
        set => this.RaiseAndSetIfChanged(ref _isSecondarySelected, value);
    }

    public Bitmap Icon { get; }
    public ObservableCollection<RuneSlotViewModel> Slots { get; }
    public RuneViewModel? Keystone => Slots.FirstOrDefault()?.SelectedRune;

    public RuneTreeViewModel(RuneTreeModel model, RunePageModel? page = null)
    {
        Model = model;
        Slots = new ObservableCollection<RuneSlotViewModel>(
            model.Slots.Select(s => new RuneSlotViewModel(s))
        );
        Icon = PathUtils.PathToBitMap(model.IconPath);
        if (page == null) return;
        // foreach (var slot in Slots)
        // {
        //     foreach (var rune in slot.Runes)
        //     {
        //         if (rune.Id == page.KeystoneId || page.PrimaryRuneIds.Contains(rune.Id))
        //             rune.IsSelected = true;
        //     }
        // }
    }
}