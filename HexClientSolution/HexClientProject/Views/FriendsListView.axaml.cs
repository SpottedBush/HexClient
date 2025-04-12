using System;
using Avalonia.Controls;
using Avalonia.Input;
using HexClientProject.Models;

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
    }
}