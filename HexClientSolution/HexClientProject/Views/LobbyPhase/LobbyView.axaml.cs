using Avalonia.Controls;
using Avalonia.Interactivity;
using HexClientProject.ViewModels.LobbyPhase;
using HexClientProject.ViewModels.ViewManagement;

namespace HexClientProject.Views.LobbyPhase
{
    public partial class LobbyView : UserControl
    {
        
        public LobbyView()
        {
            InitializeComponent();
            DataContext = new LobbyViewModel();
        }

        private void ShowRoleMenu1(object sender, RoutedEventArgs e)
        {
            if (RoleMenu1 == null || sender is not Control button) return;
            RoleMenu1.PlacementTarget = button;
            RoleMenu1.Open();
        }

        private void ShowRoleMenu2(object sender, RoutedEventArgs e)
        {
            if (RoleMenu2 == null || sender is not Control button) return;
            RoleMenu2.PlacementTarget = button;
            RoleMenu2.Open();
        }
    }
}
