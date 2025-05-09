using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HexClientProject.Models.RuneSystem;

namespace HexClientProject.ViewModels.RuneSystem;

public class StatPerksViewModel
{
    public ObservableCollection<RuneViewModel> Perks { get; }

    public StatPerksViewModel(List<RuneModel> models)
    {
        Perks = new ObservableCollection<RuneViewModel>(
            models.Select(m => new RuneViewModel(m))
        );
    }
}