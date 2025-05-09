using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using Tmds.DBus.Protocol;

namespace HexClientProject.Models.RuneSystem;

public class RunePageModel
{
    public string PrimaryTreeName { get; set; }
    public string SecondaryTreeName { get; set; }

    public RuneModel Keystone { get; set; }
    public List<RuneModel> PrimaryRunes { get; set; } = new();
    public List<RuneModel> SecondaryRunes { get; set; } = new();
    public List<RuneModel> StatPerks { get; set; } = new();


    public RunePageModel(List<int> runeIds)
    {
        for (int i = 0; i < runeIds.Count; i++)
        {
            if (i == 0)
            {
                Keystone = new RuneModel { Id = runeIds[i] };
            }
            else if (i < 4)
            {
                PrimaryRunes.Add(new RuneModel { Id = runeIds[i] });
            }
            else if (i < 6)
            {
                SecondaryRunes.Add(new RuneModel { Id = runeIds[i] });
            }
            else
            {
                StatPerks.Add(new RuneModel { Id = runeIds[i] });
            }
        }
    }

    public List<int> GetAllRuneIds()
    {
        List<int> allRuneIds = new List<int> { };
        allRuneIds.Append(Keystone.Id);
        
        foreach (RuneModel r in PrimaryRunes)
        {
            allRuneIds.Append(r.Id);
        }

        foreach (RuneModel r in SecondaryRunes)
        {
            allRuneIds.Append(r.Id);
        }

        foreach (RuneModel r in StatPerks)
        {
            allRuneIds.Append(r.Id);
        }

        return allRuneIds;
    } 
}

public class RuneModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Bitmap? IconPath { get; set; }
}
