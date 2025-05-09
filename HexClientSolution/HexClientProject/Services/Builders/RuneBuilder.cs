using System.Collections.Generic;
using System.Threading.Tasks;
using HexClientProject.Interfaces;
using HexClientProject.Models;
using HexClientProject.Models.RuneSystem;

namespace HexClientProject.Services.Builders;

public class RuneBuilder : IRuneService
{
    public Task<List<RuneTreeModel>> GetAllTrees()
    {
        throw new System.NotImplementedException();
    }

    public void CreateRunePage()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateRunePage(int pageId)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteRunePage(int pageId)
    {
        throw new System.NotImplementedException();
    }

    public void RenameRunePage(int pageId)
    {
        throw new System.NotImplementedException();
    }

    public void GetPageInventory()
    {
        throw new System.NotImplementedException();
    }

    public Task LoadRunePages()
    {
        throw new System.NotImplementedException();
    }

    public void SaveRunePages(IEnumerable<RunePageModel> pages)
    {
        throw new System.NotImplementedException();
    }
}