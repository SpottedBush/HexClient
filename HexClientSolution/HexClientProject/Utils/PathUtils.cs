using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace HexClientProject.Utils;

public static class PathUtils
{
    public static string GetAssetDirectory()
    {
        return "avares://HexClientProject/Assets/";
    }

    // Path should be the path starting at Assets/
    public static Bitmap PathToBitMap(string path)
    {
        return new Bitmap(AssetLoader.Open(new Uri(Path.Combine(GetAssetDirectory(), path))));
    }
    
    public static Bitmap RunePathToBitMap(string runePath)
    {
        return PathToBitMap(Path.Combine("perk-images", "Styles", runePath));
    }
    
    public static Bitmap StatModsPathToBitMap(string runePath)
    {
        return PathToBitMap(Path.Combine("perk-images", "StatMods", runePath));
    }
}