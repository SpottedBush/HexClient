using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.Models;
using HexClientProject.Views;
using ReactiveUI;

namespace HexClientProject.ViewModels
{
    public partial class LobbyViewModel : ReactiveObject
    {
        private readonly StateManager _stateManager = StateManager.Instance;
        private readonly DispatcherTimer _timer;
        private int _secondsElapsed;
        private readonly MatchFoundViewModel _matchFoundVm;

        // Commands
        public ReactiveCommand<Unit, Unit> StartQueueCommand { get; }
        public ReactiveCommand<Unit, Unit> LeaveQueueCommand { get; }
        public ReactiveCommand<Unit, Unit> ReturnToGameModeCommand { get; }
        public ICommand AssignRole1Command { get; set; }
        public ICommand AssignRole2Command { get; set; }

        // Summoners Info related properties
        private string _gameModeName = string.Empty;
        public string GameModeName
        {
            get => _gameModeName;
            set => this.RaiseAndSetIfChanged(ref _gameModeName, value);
        }

        private string _lobbyName = string.Empty;
        public string LobbyName
        {
            get => _lobbyName;
            set => this.RaiseAndSetIfChanged(ref _lobbyName, value);
        }

        private string _summonerName = string.Empty;
        public string SummonerName
        {
            get => _summonerName;
            set
            {
                this.RaiseAndSetIfChanged(ref _summonerName, value);
                this.RaisePropertyChanged(nameof(DisplayText));
            }
        }

        private int _summonerLevel;
        public int SummonerLevel
        {
            get => _summonerLevel;
            set
            {
                this.RaiseAndSetIfChanged(ref _summonerLevel, value);
                this.RaisePropertyChanged(nameof(DisplayText));
            }
        }

        private string _summonerRank = string.Empty;
        public string SummonerRank
        {
            get => _summonerRank;
            set => this.RaiseAndSetIfChanged(ref _summonerRank, value);
        }

        private string _summonerDivision = string.Empty;
        public string SummonerDivision
        {
            get => _summonerDivision;
            set => this.RaiseAndSetIfChanged(ref _summonerDivision, value);
        }

        public ObservableCollection<LobbyPlayerViewModel> Summoners { get; set; } = new();

        public string DisplayText => $"{SummonerName} (Level {SummonerLevel}) Rank: {SummonerRank} {SummonerDivision}";

        private string _selectedRole1 = "none";
        public string SelectedRole1
        {
            get => _selectedRole1;
            set => this.RaiseAndSetIfChanged(ref _selectedRole1, value);
        }

        private string _selectedRole2 = "none";
        public string SelectedRole2
        {
            get => _selectedRole2;
            set => this.RaiseAndSetIfChanged(ref _selectedRole2, value);
        }

        private Bitmap _selectedRole1Image = new Bitmap(AssetLoader.Open(new Uri("avares://HexClientProject/Assets/roles/none_icon.png")));
        public Bitmap SelectedRole1Image
        {
            get => _selectedRole1Image;
            set => this.RaiseAndSetIfChanged(ref _selectedRole1Image, value);
        }

        private Bitmap _selectedRole2Image = new Bitmap(AssetLoader.Open(new Uri("avares://HexClientProject/Assets/roles/none_icon.png")));
        public Bitmap SelectedRole2Image
        {
            get => _selectedRole2Image;
            set => this.RaiseAndSetIfChanged(ref _selectedRole2Image, value);
        }

        
        // Timer-related properties
        private string? _timerDisplay;
        public string? TimerDisplay
        {
            get => _timerDisplay;
            set => this.RaiseAndSetIfChanged(ref _timerDisplay, value);
        }

        private bool _isQueueButtonEnabled = true;
        public bool IsQueueButtonEnabled
        {
            get => _isQueueButtonEnabled;
            set => this.RaiseAndSetIfChanged(ref _isQueueButtonEnabled, value);
        }

        private bool _isCancelButtonEnabled;
        public bool IsCancelButtonEnabled
        {
            get => _isCancelButtonEnabled;
            set => this.RaiseAndSetIfChanged(ref _isCancelButtonEnabled, value);
        }

        private bool _isTimerEnabled;
        public bool IsTimerEnabled
        {
            get => _isTimerEnabled;
            set => this.RaiseAndSetIfChanged(ref _isTimerEnabled, value);
        }

        public bool IsPopupVisible { get; set; } = false;

        private void StartQueue()
        {
            _secondsElapsed = 0;
            TimerDisplay = "00:00";
            IsQueueButtonEnabled = false;
            IsCancelButtonEnabled = true;
            IsTimerEnabled = true;
            _timer.Start();
        }

        private void LeaveQueue()
        {
            _timer.Stop();
            TimerDisplay = "00:00";
            _secondsElapsed = 0;
            IsQueueButtonEnabled = true;
            IsCancelButtonEnabled = false;
            IsTimerEnabled = false;
        }

        private void AcceptMatch()
        {
            // _stateManager.LeftPanelContent = new DraftView(); // Switch the view
            _timer.Stop();
        }

        private void ResumeQueue()
        {
            _timer.Start();
        }

        public LobbyViewModel(MainViewModel mainViewModel)
        {
            _matchFoundVm = new MatchFoundViewModel
            {
                OnRefuseOrTimeoutMatchRequested = LeaveQueue,
                OnAcceptMatchRequested = AcceptMatch,
                OnCancelMatchRequested = ResumeQueue,
            };
            TimerDisplay = "00:00";

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += TimerTick;
            
            for (int i = 1; i < _stateManager.LobbyInfo.Summoners!.Count; i++) // Skipping curr player
            {
                Summoners.Add(new LobbyPlayerViewModel(i));
            }

            _gameModeName = _stateManager.LobbyInfo.CurrSelectedGameModeModel.GameModeDescription;
            _lobbyName = _stateManager.LobbyInfo.LobbyName;
            _summonerName = _stateManager.SummonerInfo.GameName;
            _summonerLevel = _stateManager.SummonerInfo.SummonerLevel;
            _summonerRank = SummonerInfoViewModel.RankStrings[_stateManager.SummonerInfo.RankId];
            _summonerDivision = SummonerInfoViewModel.RankDivisions[_stateManager.SummonerInfo.RankId];
            ReturnToGameModeCommand = ReactiveCommand.Create(() => { _stateManager.LeftPanelContent = new GameModeSelectionView(mainViewModel);});
            StartQueueCommand = ReactiveCommand.Create(StartQueue);
            LeaveQueueCommand = ReactiveCommand.Create(LeaveQueue);
            AssignRole1Command = ReactiveCommand.Create<string>(role =>
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

            AssignRole2Command = ReactiveCommand.Create<string>(role =>
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

        private async void TimerTick(object? sender, EventArgs e)
        {
            _secondsElapsed++;
            TimerDisplay = TimeSpan.FromSeconds(_secondsElapsed).ToString(@"mm\:ss");
            if (_stateManager.IsOnlineMode)
            {
                // TODO LOUIS: Get Match found signal if online mode
            }
            if (_secondsElapsed == 2 || _secondsElapsed == 8) 
            {
                ShowMatchFoundPopup();
                _timer.Stop();
            }
        }
        private void ShowMatchFoundPopup()
        {
            var matchFoundWindow = new Window
            {
                Width = 300,
                Height = 200,
                Content = new MatchFoundView(_matchFoundVm),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            _matchFoundVm.CloseMatchFoundPopUpRequest += () =>
            {
                matchFoundWindow.Close();
            };
            _matchFoundVm.Start();
            matchFoundWindow.ShowDialog(App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : null);
        }

    }
}