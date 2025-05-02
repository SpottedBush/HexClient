using System.Collections.Generic;
using HexClientProject.Models;
using ReactiveUI;

namespace HexClientProject.ViewModels.Rune;

public class RuneEditorViewModel(RunePageModel model) : ReactiveObject
{
    public string Name
    {
        get => model.PageName;
        set
        {
            model.PageName = value;
            this.RaisePropertyChanged();
        }
    }

    public RuneTree PrimaryTree => model.PrimaryTree;
    public RuneTree SecondaryTree => model.SecondaryTree;
    public List<BonusStat> BonusStats => model.BonusStats;
}
