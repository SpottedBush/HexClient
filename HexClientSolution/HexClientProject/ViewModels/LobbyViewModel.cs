using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HexClientProject.ViewModels
{
    public partial class LobbyViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<SummonerViewModel> summoners;

        public ICommand AssignRoleCommand { get; }

        public LobbyViewModel()
        {
            Summoners = new ObservableCollection<SummonerViewModel>
            {
                new SummonerViewModel("Player 1", Brushes.LightGray),
                new SummonerViewModel("Player 2", Brushes.Black),
                new SummonerViewModel("Player 3", Brushes.LightGray),
                new SummonerViewModel("Player 4", Brushes.Black),
                new SummonerViewModel("Player 5", Brushes.LightGray)
            };

            AssignRoleCommand = new RelayCommand<string>(AssignRole);
        }

        private void AssignRole(string role)
        {
            // Handle role assignment logic
        }
    }

    public class SummonerViewModel : ObservableObject
    {
        public string Name { get; }
        public IBrush RowColor { get; }

        public SummonerViewModel(string name, IBrush rowColor)
        {
            Name = name;
            RowColor = rowColor;
        }
    }
}