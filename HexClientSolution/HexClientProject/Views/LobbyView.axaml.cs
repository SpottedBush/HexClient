using Avalonia.Controls;
using HexClientProject.ViewModels;

namespace HexClientProject.Views
{
    public partial class LobbyView : UserControl
    {
        public LobbyView()
        {
            InitializeComponent();
            DataContext = new LobbyViewModel(); // Ensure it has a ViewModel
        }
    }
}