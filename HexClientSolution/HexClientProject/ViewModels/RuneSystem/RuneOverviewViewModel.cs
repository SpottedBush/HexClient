using HexClientProject.Models.RuneSystem;
using HexClientProject.Utils;
using ReactiveUI;

namespace HexClientProject.ViewModels.RuneSystem;

public class RuneOverviewViewModel : ReactiveObject
{
    public RunePageModel RunePage { get; }

    public RuneOverviewViewModel()
    {
        RunePage = new RunePageModel
        {
            PrimaryTreeName = "Precision",
            SecondaryTreeName = "Domination",
            Keystone = new RuneModel { Id = 8005, Name = "Press the Attack", IconPath = PathUtils.RunePathToBitMap("Precision/PressTheAttack/PressTheAttack.png") },
            PrimaryRunes =
            [
                new RuneModel() { Id = 9101, Name = "Absorb Life", IconPath = PathUtils.RunePathToBitMap("Precision/AbsorbLife/AbsorbLife.png") },
                new RuneModel() { Id = 9111, Name = "Triumph", IconPath = PathUtils.RunePathToBitMap("Precision/Triumph/Triumph.png") },
                new RuneModel() { Id = 8014, Name = "Coup de Grace", IconPath = PathUtils.RunePathToBitMap("Precision/CoupDeGrace/CoupDeGrace.png") }
            ],
            SecondaryRunes =
            [
                new RuneModel() { Id = 8126, Name = "Cheap Shot", IconPath = PathUtils.RunePathToBitMap("Domination/CheapShot/CheapShot.png") },
                new RuneModel()
                {
                    Id = 8138, Name = "Deep Ward", IconPath = PathUtils.RunePathToBitMap("Domination/DeepWard/DeepWard.png")
                }
            ],
            StatPerks =
            [
                new RuneModel() { Id = 5008, Name = "Adaptive Force", IconPath = PathUtils.StatModsPathToBitMap("StatModsAdaptiveForceIcon.png") },
                new RuneModel() { Id = 5002, Name = "Armor", IconPath = PathUtils.StatModsPathToBitMap("StatModsArmorIcon.png") },
                new RuneModel() { Id = 5003, Name = "Magic Resist", IconPath = PathUtils.StatModsPathToBitMap("StatModsMagicResIcon.png") }
            ]
        };
    }
}
