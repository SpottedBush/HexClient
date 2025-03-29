using System;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.ViewModels;

namespace HexClientProject.Views
{
    public partial class LobbyView : UserControl
    {
        
        
        public LobbyView(ViewModels.MainViewModel mainViewModel)
        {
            DataContext = new LobbyViewModel(mainViewModel);
            InitializeComponent();
        }

        private void ShowRoleMenu1(object sender, RoutedEventArgs e)
        {
            var button = sender as Control;
            if (RoleMenu1 != null && button != null)
            {
                RoleMenu1.PlacementTarget = button;
                RoleMenu1.Open();
            }
        }

        private void ShowRoleMenu2(object sender, RoutedEventArgs e)
        {
            var button = sender as Control;
            if (RoleMenu2 != null && button != null)
            {
                RoleMenu2.PlacementTarget = button;
                RoleMenu2.Open();
            }
        }
    }
}
