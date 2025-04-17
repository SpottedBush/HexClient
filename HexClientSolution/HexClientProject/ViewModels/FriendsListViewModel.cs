using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using ReactiveUI;

namespace HexClientProject.ViewModels;

public class FriendsListViewModel : ViewModelBase
{
    public ObservableCollection<FriendModel> Friends { get; } = new();
    private readonly StateManager _stateManager = StateManager.Instance;
    public ReactiveCommand<Unit, Unit> AddFriendCommand { get; }
    public string? NewFriendUsername { get; set; }
    private FriendModel? _selectedFriend;

    public FriendModel? SelectedFriend
    {
        get => _selectedFriend;
        set => this.RaiseAndSetIfChanged(ref _selectedFriend, value);
    }
    
    public FriendsListViewModel()
    {
        AddFriendCommand = ReactiveCommand.Create(AddFriend);
        LoadFriends();
    }

    private void LoadFriends()
    {
        _stateManager.Friends = new ObservableCollection<FriendModel>(ApiProvider.SocialService.GetFriendModelList());
        Friends.Clear();
        foreach (var f in _stateManager.Friends)
            Friends.Add(f);
    }

    private void AddFriend()
    {
        ApiProvider.SocialService.AddFriend(new FriendModel
        {
            Username = NewFriendUsername,
            Status = "RIVEN OTP",
            RankId = 1,
            DivisionId = 2
        });
    }
    public void WhisperTo(string username)
    {
        _stateManager.ChatBoxViewModel.SelectedScope = ChatScope.Whisper;
        _stateManager.ChatBoxViewModel.SelectedWhisperTarget = username;
        _stateManager.ChatBoxViewModel.MessageInput = $"/mp <{username}> ";
        _stateManager.ChatBoxViewModel.ApplyFilterToWhisper(username);
    }
}