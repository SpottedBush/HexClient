using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using HexClientProject.Models.RuneSystem;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem.RuneEditor;

public class StatPerksViewModel : ReactiveObject
{
    public ObservableCollection<ObservableCollection<StatPerkViewModel>> Rows { get; }

    public StatPerksViewModel()
    {
        // Manually defined ID layout (3 rows of 3)
        var statModIds = new[]
        {
            new[] { 5008, 5005, 5007 },
            new[] { 5008, 5010, 5001 },
            new[] { 5011, 5013, 5001 }
        };

        Rows = new ObservableCollection<ObservableCollection<StatPerkViewModel>>(
            statModIds.Select(row =>
                new ObservableCollection<StatPerkViewModel>(
                    row.Select(RuneLookupTableModel.GetStatMod)
                        .Where(m => m != null)
                        .Select(m => new StatPerkViewModel(m!))
                )
            )
        );

        // Enforce mutual exclusivity within each row
        foreach (var row in Rows)
        {
            foreach (var perk in row)
            {
                perk.WhenAnyValue(p => p.IsSelected)
                    .Where(selected => selected)
                    .Subscribe(_ => EnforceRowSelectionLimit(row, perk));
            }
        }
    }

    private void EnforceRowSelectionLimit(ObservableCollection<StatPerkViewModel> row, StatPerkViewModel selected)
    {
        foreach (var perk in row)
        {
            if (perk != selected)
                perk.IsSelected = false;
        }
    }
}