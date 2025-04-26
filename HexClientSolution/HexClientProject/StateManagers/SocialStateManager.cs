using System.Collections.ObjectModel;
using HexClientProject.Models;
using HexClientProject.ViewModels;
using ReactiveUI;

namespace HexClientProject.StateManagers;

public class SocialStateManager : ReactiveObject
{
    public ObservableCollection<FriendModel> Friends { get; } = new();
    public Collection<string> MutedUsernames { get; } = new();
    public ChatBoxViewModel ChatBoxViewModel { get; set; } = null!;
    public FriendsListViewModel FriendsListViewModel { get; set; } = null!;

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
}