using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using HexClientProject.Models;

namespace HexClientProject.ViewModels
{
    public class ChatBoxViewModel : ReactiveObject
    {
        private string _messageInput;
        public string MessageInput
        {
            get => _messageInput;
            set => this.RaiseAndSetIfChanged(ref _messageInput, value);
        }

        private ChatScope _selectedScope;
        public ChatScope SelectedScope
        {
            get => _selectedScope;
            set => this.RaiseAndSetIfChanged(ref _selectedScope, value);
        }

        public ObservableCollection<MessageModel> Messages { get; set; } = new();

        public List<ChatScope> Scopes { get; set; } = new List<ChatScope>((ChatScope[])Enum.GetValues(typeof(ChatScope)));
        public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }

        public ChatBoxViewModel()
        {
            SelectedScope = ChatScope.Global;

            Messages.Add(new MessageModel { Sender = "System", Content = "Welcome to Global Chat", Timestamp = DateTime.Now });

            SendMessageCommand = ReactiveCommand.Create(() =>
            {
                if (!string.IsNullOrWhiteSpace(MessageInput))
                {
                    Messages.Add(new MessageModel
                    {
                        Sender = "You",
                        Content = $"[{SelectedScope}] {MessageInput}",
                        Timestamp = DateTime.Now
                    });

                    MessageInput = string.Empty;
                }
            });
        }
    }
}