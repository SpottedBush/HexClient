using Avalonia.Controls;
using HexClientProject.ViewModels;

namespace HexClientProject.Views
{
    public partial class FriendView : UserControl
    {
        public FriendView()
        {
            InitializeComponent();
            DataContext = new FriendViewModel();
        }
    }
}