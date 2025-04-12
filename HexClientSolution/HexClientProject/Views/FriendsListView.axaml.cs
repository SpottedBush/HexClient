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
                if (sender is Border border && border.DataContext is FriendModel friend)
                {
                    if (DataContext is FriendsListViewModel vm)
                    {
                        vm.OpenChatWith(friend);
                    }
                }
            }
        }
    }
}