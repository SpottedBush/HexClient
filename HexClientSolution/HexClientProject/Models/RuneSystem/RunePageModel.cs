using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using Tmds.DBus.Protocol;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Avalonia.Platform;

namespace HexClientProject.Models.RuneSystem;

public class RunePageModel
{
    public string pageName { get; set; } = string.Empty;
    public int pageId { get; set; }
    public int MainTreeId { get; set; }
    public int KeystoneId { get; set; }
    public List<int> PrimaryRuneIds { get; set; }
    public int SecondaryTreeId { get; set; }
    public List<int> SecondaryRuneIds { get; set; }
    public List<int> StatModsIds { get; set; }

    public RunePageModel(List<int> selectedRuneIdList)
    {
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
    public static async Task<RunePageModel> LoadFromJsonAsync(Uri resourceUri)
    {
        var stream = AssetLoader.Open(resourceUri);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        return JsonSerializer.Deserialize<RunePageModel>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidDataException("Failed to deserialize RunePageModel");
    }

}