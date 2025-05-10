using System;
using System.Collections.Generic;
using Avalonia.Threading;
using HexClientProject.StateManagers;
using ReactiveUI;
using LobbyView = HexClientProject.Views.LobbyPhase.LobbyView;

namespace HexClientProject.ViewModels.DraftPhase;

public class DraftViewModel : ReactiveObject
{
    private readonly GlobalStateManager _globalStateManager = GlobalStateManager.Instance;
    private readonly ViewStateManager _viewStateManager = ViewStateManager.Instance;
    public string GameModeName { get; set; }
    private readonly DispatcherTimer _timer;
    private double _durationOfOfCurrentPhase;
    public double DurationOfCurrentPhase
    {
        get => _durationOfOfCurrentPhase;
        set => this.RaiseAndSetIfChanged(ref _durationOfOfCurrentPhase, value);
    }
    private double _timeLeft;
    public double TimeLeft
    {
        get => _timeLeft;
        set => this.RaiseAndSetIfChanged(ref _timeLeft, value);
    }
    private string _displayTimer;
    public string DisplayTimer
    {
        get => _displayTimer;
        set => this.RaiseAndSetIfChanged(ref _displayTimer, value);
    }
    public List<string> LeftTeamPlayers { get; set; } = new(); // TODO: Change to a list of players
    public List<string> RightTeamPlayers { get; set; } = new();
    public DraftViewModel()
    {
        _globalStateManager.IsInDraft = true;
        GameModeName = _globalStateManager.LobbyInfo.CurrSelectedGameModeModel.GameModeDescription;
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(25)
        };
        StartAllPhaseRoutine();
        _displayTimer = "Time left: " + _timeLeft;
    }

    private void StartAllPhaseRoutine()
    {
        Start(1000); //Ban Phase
        // Wait
        // Pre-pick phase... (depends on the gameMode!)
    }
    
    private void Start(double timerDuration)
    {
        DurationOfCurrentPhase = timerDuration;
        TimeLeft = timerDuration;
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }
    
    private void OnTimerTick(object? sender, EventArgs e)
    {
        TimeLeft -= 0.025;
        DisplayTimer = $"Time left: {_timeLeft:0}s";

        if (!(_timeLeft <= 0)) return;
        StopTimer();
        _globalStateManager.IsInDraft = false;
        _viewStateManager.CurrView = new LobbyView();
    }
    
    private void StopTimer()
    {
        _timer.Tick -= OnTimerTick;
        _timer.Stop();
    }
}