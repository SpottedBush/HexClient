using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Platform;
using HexClientProject.Models.RuneSystem;

namespace HexClientProject.Utils;

public static class JsonLoaderUtils
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    // Loads the entirety of the rune system (i.e., all the rune trees)
    public static async Task<List<RuneTreeModel>> LoadRuneTreesFromJsonAsync(string path)
    {
        var uri = new Uri(path);

        if (!AssetLoader.Exists(uri))
            throw new FileNotFoundException($"Rune JSON not found: {path}");

        await using var stream = AssetLoader.Open(uri);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        try
        {
            var trees = JsonSerializer.Deserialize<List<RuneTreeModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new();
            return trees;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON deserialization failed: {ex.Message}");
            return new();
        }
    }

    public static async Task<List<RuneModel>> LoadStatModsFromJsonAsync(string path)
    {
        var uri = new Uri(path);
        if (!AssetLoader.Exists(uri))
            throw new FileNotFoundException($"Stat Mod JSON not found: {path}");
        
        await using var stream = AssetLoader.Open(uri);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        try
        {
            var statMods = JsonSerializer.Deserialize<List<RuneModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new();
            return statMods;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON deserialization failed: {ex.Message}");
            return new();
        }
    }
}