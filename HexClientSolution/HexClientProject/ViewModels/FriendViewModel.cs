using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using HexClientProject.Models;
using ReactiveUI;

namespace HexClientProject.ViewModels
{
    public class FriendViewModel : ViewModelBase
    {
        private string _friendUsername;
        private string _chatMessage;
        private FriendModel? _selectedFriend;

        public ObservableCollection<FriendModel> Friends { get; } = new();
        public ObservableCollection<string> ChatMessages { get; } = new();

        public string NewFriendUsername
        {
            get => _friendUsername;
            set => this.RaiseAndSetIfChanged(ref _friendUsername, value);
        }

        public string ChatMessage
        {
            get => _chatMessage;
            set => this.RaiseAndSetIfChanged(ref _chatMessage, value);
        }

        public FriendModel? SelectedFriend
        {
            get => _selectedFriend;
            set => this.RaiseAndSetIfChanged(ref _selectedFriend, value);
        }

        public ReactiveCommand<Unit, Unit> RemoveFriendCommand { get; }
        public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }

        public FriendViewModel()
        {
            RemoveFriendCommand = ReactiveCommand.Create(RemoveFriend);
            SendMessageCommand = ReactiveCommand.Create(SendMessage);
            SelectedFriend = new FriendModel { Username = "HisRiven", Status = "RIVEN OTP", RankId = 1, DivisionId = 2 };
        }

        private void RemoveFriend()
        {
            if (SelectedFriend != null)
            {
                Friends.Remove(SelectedFriend);
                SelectedFriend = null;
            }
        }

        private void SendMessage()
        {
            if (SelectedFriend != null && !string.IsNullOrWhiteSpace(ChatMessage))
            {
                ChatMessage = string.Empty;
            }
        }
    }
}
