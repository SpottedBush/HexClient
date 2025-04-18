using System;
using System.Collections.ObjectModel;
using System.Reactive;
using HexClientProject.Models;
using HexClientProject.Services.Providers;
using ReactiveUI;

namespace HexClientProject.ViewModels;

public class FriendsListViewModel : ViewModelBase
{
    private readonly StateManager _stateManager = StateManager.Instance;
    public ObservableCollection<FriendModel> Friends => _stateManager.Friends;
    public ReactiveCommand<Unit, Unit> AddFriendCommand { get; }
    private string _newFriendUsername = string.Empty;
    public string NewFriendUsername 
    {
        get => _newFriendUsername;
        set => this.RaiseAndSetIfChanged(ref _newFriendUsername, value);
    }
    private FriendModel? _selectedFriend;

    public FriendModel? SelectedFriend
    {
        get => _selectedFriend;
        set => this.RaiseAndSetIfChanged(ref _selectedFriend, value);
    }
    
    public FriendsListViewModel()
    {
        NewFriendUsername = String.Empty;
        AddFriendCommand = ReactiveCommand.Create(AddFriend);
        LoadFriends();
    }

    private void LoadFriends()
    {
        var newFriends = ApiProvider.SocialService.GetFriendModelList();
        _stateManager.Friends.Clear();
        foreach (var friend in newFriends)
            _stateManager.Friends.Add(friend);
    }

    private void AddFriend()
    {
        bool success = ApiProvider.SocialService.AddFriend(NewFriendUsername);
        if (success)
            LoadFriends();
        NewFriendUsername = String.Empty; // Clear the textbox
    }
    public void WhisperTo(string username)
    {
        _stateManager.ChatBoxViewModel.SelectedScope = ChatScope.Whisper;
        _stateManager.ChatBoxViewModel.SelectedWhisperTarget = username;
        _stateManager.ChatBoxViewModel.MessageInput = $"/mp <{username}> ";
        _stateManager.ChatBoxViewModel.ApplyFilterToWhisper(username);
    }
}