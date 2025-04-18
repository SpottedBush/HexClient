using System;
using Avalonia.Controls;
using Avalonia.Input;
using HexClientProject.Models;
using HexClientProject.ViewModels;

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
            if (e.GetCurrentPoint(null).Properties.IsLeftButtonPressed)
            {
                var border = sender as Border;
                if (border != null)
                {
                    var friend = border.DataContext as FriendModel;
                    if (friend != null)
                    {
                        if (DataContext is FriendsListViewModel vm)
                        {
                            vm.WhisperTo(friend.Username);
                        }
                    }
                }
            }
        }
        private void TextBox_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is FriendsListViewModel vm)
                {
                    vm.AddFriendCommand.Execute().Subscribe();
                }
            }
        }

    }
}