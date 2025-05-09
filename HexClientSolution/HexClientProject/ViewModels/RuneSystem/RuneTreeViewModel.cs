using HexClientProject.Models.RuneSystem;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneTreeViewModel : ReactiveObject
{
    public string Name { get; }
    public string IconPath { get; }

    public ObservableCollection<RuneSlotViewModel> Slots { get; }

    public RuneTreeViewModel(RuneTreeModel model)
    {
        Name = model.Name;
        IconPath = model.IconPath;
        Slots = new ObservableCollection<RuneSlotViewModel>(
            model.Slots.Select(s => new RuneSlotViewModel(s))
        );
    }

    public RuneViewModel? Keystone => Slots.FirstOrDefault()?.SelectedRune;
}