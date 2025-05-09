using Avalonia.Controls;
using HexClientProject.ViewModels.RuneSystem;

namespace HexClientProject.Views.Rune
{
    public partial class RuneOverviewView : UserControl
    {
        public RuneOverviewView()
        {
            InitializeComponent();
            DataContext = new RuneOverviewViewModel();
        }

        // Optional: Handle any events or commands like EditRunePage
        // If using navigation or view switching, you may inject a navigation service or handle event here
    }
}