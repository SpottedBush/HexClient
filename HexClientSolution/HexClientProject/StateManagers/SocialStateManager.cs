using System.Collections.ObjectModel;
using HexClientProject.Models;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.SideBar;
using ReactiveUI;

namespace HexClientProject.StateManagers;

public class SocialStateManager : ReactiveObject
{
    private static SocialStateManager? _instance;
    // Public property to access the single instance
    public static SocialStateManager Instance
    {
        get
        {
            _instance ??= new SocialStateManager();
            return _instance;
        }
    }
    public ObservableCollection<FriendModel> Friends { get; set; } = [];
    public Collection<string> MutedUsernames { get; } = [];
    public ChatBoxViewModel ChatBoxViewModel { get; set; } = null!;
    public FriendsListViewModel FriendsListViewModel { get; set; } = null!;

}