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
        Console.WriteLine(path);
        return new Bitmap(AssetLoader.Open(new Uri(Path.Combine(GetAssetDirectory(), path))));
    }
}