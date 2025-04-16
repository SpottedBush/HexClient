using System;
using Avalonia.Threading;
using ReactiveUI;
using System.Reactive;
using HexClientProject.Models;

namespace HexClientProject.ViewModels;

public class MatchFoundViewModel : ReactiveObject
{
    private readonly StateManager _stateManager = StateManager.Instance;
    private readonly DispatcherTimer _timer;

    private double _timeLeft;
    public double TimeLeft
    {
        get => _timeLeft;
        set => this.RaiseAndSetIfChanged(ref _timeLeft, value);
    }

    private bool _isActionDone;
    public bool IsActionDone
    {
        get => _isActionDone;
        set => this.RaiseAndSetIfChanged(ref _isActionDone, value);
    }

    private bool _isMatchAccepted;

    public Action? CloseMatchFoundPopUpRequest { get; set; }
    public Action? OnRefuseOrTimeoutMatchRequested { get; set; }
    public Action? OnAcceptMatchRequested { get; set; }
    public Action? OnCancelMatchRequested { get; set; }

    public ReactiveCommand<Unit, Unit> AcceptMatchCommand { get; }
    public ReactiveCommand<Unit, Unit> RefuseMatchCommand { get; }

    public MatchFoundViewModel()
    {
        AcceptMatchCommand = ReactiveCommand.Create(AcceptMatch);
        RefuseMatchCommand = ReactiveCommand.Create(RefuseMatch);

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(25)
        };
    }

    public void Start()
    {
        _timer.Tick += OnTimerTick;
        TimeLeft = 12;
        IsActionDone = false;
        _timer.Start();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        TimeLeft -= 0.025;

        if (TimeLeft <= 0)
        {
            StopTimer();
            if (!IsActionDone)
                RefuseMatch(); // Timeout is treated like a refusal
        }
    }

    private void StopTimer()
    {
        _timer.Tick -= OnTimerTick;
        _timer.Stop();
    }

    private void AcceptMatch()
    {
        StopTimer();
        IsActionDone = true;
        _isMatchAccepted = true;

        if (_stateManager.IsOnlineMode)
        {
            OnCancelMatchRequested?.Invoke(); // Assume server will notify if match fails
        }
        OnAcceptMatchRequested?.Invoke();
        CloseMatchFoundPopUpRequest?.Invoke();
    }

    private void RefuseMatch()
    {
        StopTimer();
        IsActionDone = true;
        _isMatchAccepted = false;

        OnRefuseOrTimeoutMatchRequested?.Invoke();
        CloseMatchFoundPopUpRequest?.Invoke();
    }
}
