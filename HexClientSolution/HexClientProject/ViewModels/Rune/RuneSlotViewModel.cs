using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;

namespace HexClientProject.ViewModels.Rune;

public class RuneSlotViewModel : ReactiveObject
{
    public ObservableCollection<RuneOptionViewModel> Options { get; } = [];

    public RuneOptionViewModel? SelectedOption
    {
        get => Options.FirstOrDefault(x => x.IsSelected);
        set
        {
            foreach (var opt in Options)
                opt.IsSelected = (opt == value);
            this.RaisePropertyChanged();
        }
    }
}