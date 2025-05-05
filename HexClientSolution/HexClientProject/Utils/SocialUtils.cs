

using System.Collections.ObjectModel;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;

namespace HexClientProject.Utils;

public static class SocialUtils
{

    public static void ViewProfile(string gameNameTag)
    {
        if (gameNameTag == string.Empty)
            return;
        ApiProvider.SocialService.ViewProfile(gameNameTag);
    }
}