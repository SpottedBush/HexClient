using System.Collections.Generic;
using System.Threading.Tasks;
using HexClientProject.Models.RuneSystem;

namespace HexClientProject.Interfaces;

public interface IRuneService
{
    public Task<List<RuneTreeModel>> GetAllTrees();
    public void CreateRunePage(); // Creates a RunePage for the LCU and select it as current
    public void UpdateRunePage(int pageId); // Update pageId
    public void DeleteRunePage(int pageId); // Delete pageId
    public void RenameRunePage(int pageId, string newPageName); // Rename pageId
    public void GetPageInventory(); // OwnedPageCount and MaxPageCount
    public Task LoadRunePages(); // Sync the rune pages with the LCU
    public void SaveRunePages(IEnumerable<RunePageModel> pages); // Save all the rune pages to the LCU
}