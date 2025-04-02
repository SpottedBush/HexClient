using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.Models;
using HexClientProject.Views;

namespace HexClientProject.ViewModels
{
    public partial class LobbyViewModel : ObservableObject
    {
        private static readonly StateManager StateManager = StateManager.Instance;
        
        [ObservableProperty]
        private string _gameModeName = StateManager.LobbyInfo.CurrSelectedGameModeModel.GameModeDescription;

        [ObservableProperty]
        private string _lobbyName = StateManager.LobbyInfo.LobbyName;

        [ObservableProperty]
        private string _summonerName = StateManager.SummonerInfo.GameName;
        
        [ObservableProperty]
        private int _summonerLevel = StateManager.SummonerInfo.SummonerLevel;

        [ObservableProperty]
        private string _summonerRank = SummonerInfoViewModel.RankStrings[StateManager.SummonerInfo.RankId];
        
        [ObservableProperty]
        private string _summonerDivision = SummonerInfoViewModel.RankDivisions[StateManager.SummonerInfo.RankId];

        public string DisplayText => $"{SummonerName} (Level {SummonerLevel}) Rank: {SummonerRank}{SummonerDivision}";

        partial void OnSummonerNameChanged(string value) => OnPropertyChanged(nameof(DisplayText));
        partial void OnSummonerLevelChanged(int value) => OnPropertyChanged(nameof(DisplayText));
        
        [ObservableProperty]
        private string _selectedRole1 = "autofill"; // Default role

        [ObservableProperty]
        private string _selectedRole2 = "autofill"; // Default role

        [ObservableProperty]
        private string _selectedRole1Image = "/Assets/roles/autofill_icon.png";

        [ObservableProperty]
        private string _selectedRole2Image = "/Assets/roles/autofill_icon.png";

        public ICommand AssignRole1Command { get; set; }
        public ICommand AssignRole2Command { get; set; }
        public ICommand ReturnToGameModeCommand { get; }

        [ObservableProperty]
        private ObservableCollection<SummonerViewModel> _summoners;

        public LobbyViewModel(MainViewModel mainViewModel)
        {
            ReturnToGameModeCommand = new RelayCommand(mainViewModel.SwitchToGameModeSelection);

            AssignRole1Command = new RelayCommand<string>(role =>
            {
                SelectedRole1 = role;
                SelectedRole1Image = $"/Assets/roles/{role}_icon.png";
            });

            AssignRole2Command = new RelayCommand<string>(role =>
            {
                SelectedRole2 = role;
                SelectedRole2Image = $"/Assets/roles/{role}_icon.png";
            });
           
            Summoners = new ObservableCollection<SummonerViewModel>
            {
                new SummonerViewModel("Player 1", Brushes.LightGray),
                new SummonerViewModel("Player 2", Brushes.Black),
                new SummonerViewModel("Player 3", Brushes.LightGray),
                new SummonerViewModel("Player 4", Brushes.Black),
                new SummonerViewModel("Player 5", Brushes.LightGray)
            };
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