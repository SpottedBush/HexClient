using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using HexClientProject.Models;
using HexClientProject.Utils;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.SideBar;

namespace HexClientProject.Views
{
    public partial class FriendsListView : UserControl
    {
        public FriendsListView()
        {
            InitializeComponent();
            DataContext = new FriendsListViewModel();
        }
        private void OnFriendClicked(object sender, PointerPressedEventArgs e)
        {
            if (!e.GetCurrentPoint(null).Properties.IsLeftButtonPressed) return;
            if (sender is not Border { DataContext: FriendModel friend }) return;
            if (DataContext is FriendsListViewModel)
            {
                SocialUtils.WhisperTo(friend.GameNameTag);
            }
        }
        private void TextBox_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (DataContext is FriendsListViewModel vm)
            {
                vm.AddFriendCommand.Execute().Subscribe();
            }
        }
        
        private void Popup_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is not Control control) return;
            // Walk up the visual tree to find the control that owns the flyout
            var parent = control;
            while (parent != null)
            {
                var flyout = FlyoutBase.GetAttachedFlyout(parent);
                if (flyout != null)
                {
                    flyout.Hide();
                    break;
                }
                parent = parent.Parent as Control;
            }
        }

    }
}