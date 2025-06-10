using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using HexClientProject.Models.RuneSystem;
using HexClientProject.ViewModels.RuneSystem.RuneEditor;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneSlotViewModel : ReactiveObject
{
    public ObservableCollection<RuneViewModel> Runes { get; }

    public RuneSlotViewModel(RuneSlotModel model)
    {
        Runes = new ObservableCollection<RuneViewModel>(
            model.Runes.Select(r => new RuneViewModel(r))
        );

        foreach (var rune in Runes)
        {
            rune.WhenAnyValue(x => x.IsSelected)
                .Where(isSelected => isSelected)
                .Subscribe(_ => UpdateSelection(rune));
        }
    }

    private void UpdateSelection(RuneViewModel selected)
    {
        if (!selected.IsSelected) return;
        foreach (var rune in Runes)
        {
            if (rune != selected)
            {
                rune.IsSelected = false;
            }
        }
    }

    public RuneViewModel? SelectedRune => Runes.FirstOrDefault(r => r.IsSelected);
}