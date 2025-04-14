using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using HexClientProject.Models;
using HexClientProject.ViewModels;
using ReactiveUI;

public class FriendsListViewModel : ViewModelBase
{
    public ObservableCollection<FriendModel> Friends { get; } = new();
    private readonly StateManager _stateManager = StateManager.Instance;
    public ReactiveCommand<Unit, Unit> AddFriendCommand { get; }
    public string NewFriendUsername { get; set; }
    private FriendModel _selectedFriend;

    public FriendModel SelectedFriend
    {
        get => _selectedFriend;
        set => this.RaiseAndSetIfChanged(ref _selectedFriend, value);
    }
    
    public FriendsListViewModel()
    {
        AddFriendCommand = ReactiveCommand.CreateFromTask(AddFriendAsync);
        LoadFriendsAsync().ConfigureAwait(true);
    }

    private async Task LoadFriendsAsync()
    {
        var loaded = await _stateManager.GetFriendsAsync();
        Friends.Clear();
        foreach (var f in loaded)
            Friends.Add(f);
    }

    private async Task AddFriendAsync()
    {
        bool success = await _stateManager.AddFriendAsync(NewFriendUsername);
        if (success)
        {
            await LoadFriendsAsync(); // Refresh the list
        }
    }
    public void WhisperTo(string username)
    {
        _stateManager.ChatBoxViewModel.SelectedScope = ChatScope.Whisper;
        _stateManager.ChatBoxViewModel.MessageInput = $"/mp <{username}>";
    }

}