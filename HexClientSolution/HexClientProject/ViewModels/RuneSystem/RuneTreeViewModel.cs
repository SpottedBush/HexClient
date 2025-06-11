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

    public string Name => Model.Name;
    public string IconPath => Model.IconPath;
    public bool IsSelected { get; set; } = false;
    
    public Bitmap Icon => PathUtils.PathToBitMap(IconPath);
    public ObservableCollection<RuneSlotViewModel> Slots { get; }
    public RuneViewModel? Keystone => Slots.FirstOrDefault()?.SelectedRune;

    public RuneTreeViewModel(RuneTreeModel model)
    {
        Model = model;
        Slots = new ObservableCollection<RuneSlotViewModel>(
            model.Slots.Select(s => new RuneSlotViewModel(s))
        );
    }
}