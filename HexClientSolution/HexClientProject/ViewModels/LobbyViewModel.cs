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
        private readonly StateManager _stateManager = StateManager.Instance;
        
        [ObservableProperty]
        private string _gameModeName;

        [ObservableProperty]
        private string _lobbyName;

        [ObservableProperty]
        private string _summonerName;
        
        [ObservableProperty]
        private int _summonerLevel;

        [ObservableProperty]
        private string _summonerRank;
        
        [ObservableProperty]
        private string _summonerDivision;
        
        public ObservableCollection<PlayerLineViewModel> Summoners { get; set; } = new();
        
        public string DisplayText => $"{SummonerName} (Level {SummonerLevel}) Rank: {SummonerRank} {SummonerDivision}";

        partial void OnSummonerNameChanged(string value) => OnPropertyChanged(nameof(DisplayText));
        partial void OnSummonerLevelChanged(int value) => OnPropertyChanged(nameof(DisplayText));
        
        [ObservableProperty]
        private string _selectedRole1 = "none"; // Default role

        [ObservableProperty]
        private string _selectedRole2 = "none"; // Default role

        [ObservableProperty]
        private Bitmap _selectedRole1Image = new Bitmap(AssetLoader.Open(new Uri("avares://HexClientProject/Assets/roles/none_icon.png")));

        [ObservableProperty]
        private Bitmap _selectedRole2Image = new Bitmap(AssetLoader.Open(new Uri("avares://HexClientProject/Assets/roles/none_icon.png")));

        public ICommand AssignRole1Command { get; set; }
        public ICommand AssignRole2Command { get; set; }
        public ICommand ReturnToGameModeCommand { get; }
        public ICommand StartQueueCommand { get; }
        public ICommand StopQueueCommand { get; }

        public LobbyViewModel(MainViewModel mainViewModel)
        {
            for (int i = 1; i < _stateManager.LobbyInfo.Summoners!.Count; i++) // Skipping curr player
            {
                Summoners.Add(new PlayerLineViewModel(i));
            }

            _gameModeName = _stateManager.LobbyInfo.CurrSelectedGameModeModel.GameModeDescription;
            _lobbyName = _stateManager.LobbyInfo.LobbyName;
            _summonerName = _stateManager.SummonerInfo.GameName;
            _summonerLevel = _stateManager.SummonerInfo.SummonerLevel;
            _summonerRank = SummonerInfoViewModel.RankStrings[_stateManager.SummonerInfo.RankId];
            _summonerDivision = SummonerInfoViewModel.RankDivisions[_stateManager.SummonerInfo.RankId];
            ReturnToGameModeCommand = new RelayCommand(mainViewModel.SwitchToGameModeSelection);
            StartQueueCommand = new RelayCommand(()=>
                Console.WriteLine("Start queue command")
            );
            StopQueueCommand = new RelayCommand(() =>
                Console.WriteLine("Stop queue command")
            );
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
        }
    }
}