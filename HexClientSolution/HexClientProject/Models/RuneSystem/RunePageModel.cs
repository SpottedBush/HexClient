using ReactiveUI;
using System.Collections.Generic;

namespace HexClientProject.Models.RuneSystem;

public class RunePageModel : ReactiveObject
{
    private string _pageName = string.Empty;
    public string PageName
    {
        get => _pageName;
        set => this.RaiseAndSetIfChanged(ref _pageName, value);
    }

    public int PageId { get; set; }
    public int MainTreeId { get; set; }
    public int KeystoneId { get; set; }

    public List<int> PrimaryRuneIds { get; set; }
    public int SecondaryTreeId { get; set; }
    public List<int> SecondaryRuneIds { get; set; }
    public List<int> StatModsIds { get; set; }

    public RunePageModel(List<int> selectedRuneIdList)
    {
        PrimaryRuneIds = new List<int>();
        SecondaryRuneIds = new List<int>();
        StatModsIds = new List<int>();
        for (int i = 0; i < selectedRuneIdList.Count; i++)
        {
            if (i == 0)
            {
                KeystoneId = selectedRuneIdList[i];
            }
            else if (i < 4)
            {
                PrimaryRuneIds.Add(selectedRuneIdList[i]);
            }
            else if (i < 6)
            {
                SecondaryRuneIds.Add(selectedRuneIdList[i]);
            }
            else
            {
                StatModsIds.Add(selectedRuneIdList[i]);
            }
        }
    }

    public RunePageModel(int pageId, string pageName)
    {
        PageId = pageId;
        PageName = pageName;
        PrimaryRuneIds = new List<int>();
        SecondaryRuneIds = new List<int>();
        StatModsIds = new List<int>();
    }

    public RunePageModel()
    {
        PrimaryRuneIds = new List<int>();
        SecondaryRuneIds = new List<int>();
        StatModsIds = new List<int>();
    }
}