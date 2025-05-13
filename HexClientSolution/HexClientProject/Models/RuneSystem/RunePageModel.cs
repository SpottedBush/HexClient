using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HexClientProject.Models.RuneSystem;

public class RunePageModel
{
    public string PageName { get; set; } = string.Empty;
    public int PageId { get; set; }
    public int MainTreeId { get; set; }
    public int KeystoneId { get; set; }
    public List<int> PrimaryRuneIds { get; set; }
    public int SecondaryTreeId { get; set; }
    public List<int> SecondaryRuneIds { get; set; }
    public List<int> StatModsIds { get; set; }

    
    public RunePageModel(string pageName, int pageId)
    {
        PageName = pageName;
        PageId = pageId;
    }
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

    public RunePageModel()
    {
        PrimaryRuneIds = new List<int>();
        SecondaryRuneIds = new List<int>();
        StatModsIds = new List<int>();
    }

    public void SavePageToJson(string path)
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(path, json);
    }
    public static RunePageModel LoadFromJson(JsonObject json)
    {
        return json.Deserialize<RunePageModel>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidDataException("Failed to deserialize RunePageModel");
    }
}