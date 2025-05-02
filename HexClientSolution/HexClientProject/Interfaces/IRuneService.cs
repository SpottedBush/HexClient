using System.Collections.Generic;
using HexClientProject.Models;

namespace HexClientProject.Interfaces;

public interface IRuneService
{
    public void CreateRunePageModel();
    public void ReadRunePageModel();
    public void UpdateRunePageModel();
    public void DeleteRunePageModel();

    public void LoadRunePages();
    public void SaveRunePages(IEnumerable<RunePageModel> pages);
}