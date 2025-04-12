using Avalonia.Controls;
using HexClientProject.ViewModels;

namespace HexClientProject.Views
{
    public partial class ChatBoxView : UserControl
    {
        public ChatBoxView()
        {
            InitializeComponent();
            DataContext = new ChatBoxViewModel();
        }
    }
}