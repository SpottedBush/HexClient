using System;
using System.Collections.ObjectModel;
using System.Linq;
using HexClientProject.Models.RuneSystem;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem.RuneEditor;

public class SecondaryTreeViewModel : ReactiveObject
{
    private readonly RuneTreeModel _model;
    public string Name => _model.Name;
    public int TreeId => _model.Id;
    public ObservableCollection<RuneSlotViewModel> Slots { get; }

    public SecondaryTreeViewModel(RuneTreeViewModel viewModel)
    {
        _model = viewModel.Model;
        var minorSlots = viewModel.Slots.Skip(1).Take(3).ToList();
        Slots = new ObservableCollection<RuneSlotViewModel>(minorSlots);
        foreach (var slot in Slots)
        {
            foreach (var rune in slot.Runes)
            {
                rune.WhenAnyValue(r => r.IsSelected)
                    .Subscribe(_ =>
                    {
                        EnforceSelectionLimit();
                    });
            }
        }
    }
    public SecondaryTreeViewModel(RuneTreeModel model)
    {
        _model = model;

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
                    .Subscribe(_ =>
                    {
                        EnforceSelectionLimit();
                    });
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
        var toDeselect = allSelected[1];
        toDeselect.IsSelected = false;
    }
}