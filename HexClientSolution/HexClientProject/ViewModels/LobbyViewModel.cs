using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.Models;

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
        
        public ObservableCollection<PlayerLineViewModel> Summoners { get; set; } = new();
        
        public string DisplayText => $"{SummonerName} (Level {SummonerLevel}) Rank: {SummonerRank} {SummonerDivision}";

        partial void OnSummonerNameChanged(string value) => OnPropertyChanged(nameof(DisplayText));
        partial void OnSummonerLevelChanged(int value) => OnPropertyChanged(nameof(DisplayText));
        
        [ObservableProperty]
        private string _selectedRole1 = "autofill"; // Default role

        [ObservableProperty]
        private string _selectedRole2 = "autofill"; // Default role

        [ObservableProperty]
        private Bitmap _selectedRole1Image = new Bitmap(AssetLoader.Open(new Uri("avares://HexClientProject/Assets/roles/autofill_icon.png")));

        [ObservableProperty]
        private Bitmap _selectedRole2Image = new Bitmap(AssetLoader.Open(new Uri("avares://HexClientProject/Assets/roles/autofill_icon.png")));

        public ICommand AssignRole1Command { get; set; }
        public ICommand AssignRole2Command { get; set; }
        public ICommand ReturnToGameModeCommand { get; }
        public ICommand StartQueueCommand { get; }

        public LobbyViewModel(MainViewModel mainViewModel)
        {
            for (int i = 1; i < StateManager.Instance.LobbyInfo.Summoners!.Count; i++) // Skipping curr player
            {
                Summoners.Add(new PlayerLineViewModel(i));
            }
            ReturnToGameModeCommand = new RelayCommand(mainViewModel.SwitchToGameModeSelection);

            AssignRole1Command = new RelayCommand<string>(role =>
            {
                if (role == _selectedRole2) // Role swapping
                {
                    SelectedRole2 = _selectedRole1;
                    SelectedRole2Image = SelectedRole1Image;
                    SelectedRole1 = role;
                }
                else
                    SelectedRole1 = role;
                SelectedRole1Image = new Bitmap(AssetLoader.Open(new Uri($"avares://HexClientProject/Assets/roles/{role}_icon.png")));

            });

            AssignRole2Command = new RelayCommand<string>(role =>
            {
                if (role == _selectedRole1)
                {
                    SelectedRole1 = _selectedRole2;
                    SelectedRole1Image = SelectedRole2Image;
                    SelectedRole2 = role;
                }
                else
                    SelectedRole2 = role;
                SelectedRole2Image = new Bitmap(AssetLoader.Open(new Uri($"avares://HexClientProject/Assets/roles/{role}_icon.png")));
                
            });
            [RelayCommand]
            void StartQueueCommand()
            {
                // Your logic here
                Console.WriteLine("Starting the queue...");
            }
        }
    }
}