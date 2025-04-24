using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Input;
using HexClientProject.Models;
using HexClientProject.ViewModels;

namespace HexClientProject.Views
{
    public partial class ChatBoxView : UserControl
    {
        private readonly StateManager _stateManager = StateManager.Instance;
        private readonly ChatBoxViewModel _chatBoxViewModel;
        public ChatBoxView()
        {
            InitializeComponent();
            _chatBoxViewModel = new ChatBoxViewModel();
            DataContext = _chatBoxViewModel;
            _stateManager.ChatBoxViewModel = _chatBoxViewModel;
            _chatBoxViewModel.FilteredMessages.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add && ChatListBox.Items.Count > 0)
                {
                    ChatListBox.ScrollIntoView(ChatListBox.Items[^1]!); // C# 8+ index syntax for last item
                }
            };
        }

        private void OnScopeClicked(object sender, PointerPressedEventArgs e)
        {
            var button = sender as Button;
            if (button == null || button.Content == null) return;
            var scope = button.Content;
            if (scope == null) return;
            _stateManager.ChatBoxViewModel.SelectedScope = ChatScopeExtensions.StringToScopeConverter(button.Content.ToString()!);
        }
        private void MessageInput_KeyDown(object? sender, KeyEventArgs e)
        {
            
            var textbox = sender as TextBox;
            if (textbox == null) return;
            bool changedScope = _chatBoxViewModel.CheckForScopeCommand(textbox);
            if (changedScope)
                e.Handled = true;
            
            if (e.Key == Key.Tab)
            {
                var currentScope = _stateManager.ChatBoxViewModel.SelectedScope;
                _stateManager.ChatBoxViewModel.SelectedScope =
                    ChatScopeExtensions.IntToScopeConverter(
                        (ChatScopeExtensions.ScopeToIntConverter(currentScope) + 1) % 4);
                if (currentScope == ChatScope.Party)
                {
                    _chatBoxViewModel.MessageInput = "/mp <> " + textbox.Text?.Trim();
                    textbox.Text = _chatBoxViewModel.MessageInput;
                    textbox.CaretIndex = 5;
                }
                else
                {
                    var regex = new Regex(@"^/mp\s<.*?>\s*", RegexOptions.IgnoreCase);
                    _chatBoxViewModel.MessageInput = 
                        Regex.Replace(textbox.Text?.Trim() ?? "", regex.ToString(), "");
                    textbox.Text = _chatBoxViewModel.MessageInput;
                }
                e.Handled = true;
            }

            if ((e.Key == Key.Return || e.Key == Key.Enter) && DataContext is ChatBoxViewModel vm)
            {
                vm.SendMessageCommand.Execute().Subscribe();
                e.Handled = true;
            }
        }
    }
}