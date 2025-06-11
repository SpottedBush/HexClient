using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using HexClientProject.Models.RuneSystem;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem.RuneEditor;

public class SecondaryTreeViewModel : ReactiveObject
{
    public RuneTreeModel Model;
    public string Name => Model.Name;
    public bool IsSelected { get; set; } = false;
    
    public ObservableCollection<RuneSlotViewModel> Slots { get; }

    public SecondaryTreeViewModel(RuneTreeModel model)
    {
        Model = model;

        // Only keep the 3 minor rune slots (ignore keystone)
        var minorSlots = model.Slots.Skip(1).Take(3).ToList();
        Slots = new ObservableCollection<RuneSlotViewModel>(
            minorSlots.Select(slot => new RuneSlotViewModel(slot))
        );

        foreach (var slot in Slots)
        {
            foreach (var rune in slot.Runes)
            {
                rune.WhenAnyValue(r => r.IsSelected)
                    .Where(isSelected => isSelected)
                    .Subscribe(_ =>
                    {
                        EnforceSelectionLimit();
                    });
                rune.IsSelected = true;
            }
        }
    }

    private void EnforceSelectionLimit()
    {
        var allSelected = Slots
            .SelectMany(s => s.Runes)
            .Where(r => r.IsSelected)
            .OrderBy(r => r.SelectionTime)
            .ToList();

        if (allSelected.Count <= 2) return;
        // Deselect the oldest selected rune
        var toDeselect = allSelected.Last();
        toDeselect.IsSelected = false;
    }
}