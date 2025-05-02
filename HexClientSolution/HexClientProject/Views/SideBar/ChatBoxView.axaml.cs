using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Input;
using HexClientProject.Models;
using HexClientProject.StateManagers;
using HexClientProject.ViewModels.SideBar;

namespace HexClientProject.Views.SideBar
{
    public partial class ChatBoxView : UserControl
    {
        private readonly SocialStateManager _socialStateManager = SocialStateManager.Instance;
        private readonly ChatBoxViewModel? _chatBoxViewModel;
        public ChatBoxView()
        {
            InitializeComponent();
            // Get the chatBox VM if already existing
            _chatBoxViewModel = _socialStateManager.ChatBoxViewModel ?? new ChatBoxViewModel();
            DataContext = _chatBoxViewModel;
            _socialStateManager.ChatBoxViewModel = _chatBoxViewModel;
            _chatBoxViewModel.FilteredMessages.CollectionChanged += (_, e) =>
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
            var scope = button?.Content;
            if (scope == null) return;
            _socialStateManager.ChatBoxViewModel.SelectedScope = ChatScopeExtensions.StringToScopeConverter(button!.Content!.ToString()!);
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
                var currentScope = _socialStateManager.ChatBoxViewModel.SelectedScope;
                _socialStateManager.ChatBoxViewModel.SelectedScope =
                    ChatScopeExtensions.IntToScopeConverter(
                        (ChatScopeExtensions.ScopeToIntConverter(currentScope) + 1) % 5);
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