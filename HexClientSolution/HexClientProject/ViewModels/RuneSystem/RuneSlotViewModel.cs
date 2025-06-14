using System;
using System.Collections.ObjectModel;
using System.Linq;
using HexClientProject.Models.RuneSystem;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneSlotViewModel : ReactiveObject
{
    public ObservableCollection<RuneViewModel> Runes { get; }

    private RuneViewModel? _selectedRune;
    public RuneViewModel? SelectedRune
    {
        get => _selectedRune;
        set
        {
            if (value == null)
            {
                SelectedRuneDescription = string.Empty;
            }

            this.RaiseAndSetIfChanged(ref _selectedRune, value);
        }
    }

    private string? _selectedRuneDescription;
    public string? SelectedRuneDescription
    {
        get => _selectedRuneDescription;
        private set => this.RaiseAndSetIfChanged(ref _selectedRuneDescription, value);
    }
    public RuneSlotViewModel(RuneSlotModel model)
    {
        Runes = new ObservableCollection<RuneViewModel>(
            model.Runes.Select(r => new RuneViewModel(r))
        );

        foreach (var rune in Runes)
        {
            rune.WhenAnyValue(x => x.IsSelected)
                .Subscribe(_ => UpdateSelection(rune));
        }
    }

    private void UpdateSelection(RuneViewModel selected)
    {
        if (!selected.IsSelected)
        {
            // Check if any rune is still selected
            if (!Runes.Any(r => r.IsSelected))
                SelectedRune = null;
            return;
        }
        foreach (var rune in Runes)
        {
            if (rune != selected)
            {
                rune.IsSelected = false;
            }
        }
        SelectedRune = selected;
        SelectedRuneDescription = selected.ParsedLongDesc;
    }
}