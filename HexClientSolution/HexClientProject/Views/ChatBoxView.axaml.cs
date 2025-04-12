using System;
using Avalonia.Controls;
using Avalonia.Input;
using HexClientProject.Models;

namespace HexClientProject.Views
{
    public partial class ChatBoxView : UserControl
    {
        private readonly StateManager _stateManager = StateManager.Instance;
        public ChatBoxView()
        {
            InitializeComponent();
            ChatBoxViewModel chatBoxViewModel = new ChatBoxViewModel();
            DataContext = chatBoxViewModel;
            _stateManager.ChatBoxViewModel = chatBoxViewModel;
        }
        private void MessageInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                var currentScope = _stateManager.ChatBoxViewModel.SelectedScope;
                _stateManager.ChatBoxViewModel.SelectedScope = 
                    ChatScopeExtensions.IntToScopeConverter((ChatScopeExtensions.ScopeToIntConverter(currentScope) + 1) % 3);
                e.Handled = true;
            }

            if ((e.Key == Key.Return || e.Key == Key.Enter) && DataContext is ChatBoxViewModel vm)
            {
                Console.WriteLine(e.Key);
                vm.SendMessageCommand.Execute();
                e.Handled = true;
            }
        }
    }
}