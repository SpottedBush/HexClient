using Avalonia.Controls;
using Avalonia.Interactivity;
using HexClientProject.ViewModels;
using HexClientProject.ViewModels.LobbyPhase;
using HexClientProject.ViewModels.ViewManagement;

namespace HexClientProject.Views
{
    public partial class LobbyView : UserControl
    {
        
        
        public LobbyView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = new LobbyViewModel(mainViewModel);
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
