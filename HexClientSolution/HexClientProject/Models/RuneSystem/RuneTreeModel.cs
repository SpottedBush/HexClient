using System.Collections.Generic;
using System.Linq;

namespace HexClientProject.Models.RuneSystem;


public class RuneTreeModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<RuneSlotModel> Slots { get; set; } = new();  // Only contain slots for this specific tree.
    
    private string GetTreeNameById(int treeId)
    {
        var treeNames = new List<string> { "Precision", "Domination", "Sorcery", "Resolve", "Inspiration" };
        // TODO: Get the actual treeNames
        return treeNames.ElementAtOrDefault(treeId) ?? "Unknown Tree";  // Returns the name based on the ID
    }

    public RuneTreeModel()
    {
        Name = GetTreeNameById(Id);
    }
}

