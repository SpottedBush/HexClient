using System;
using Avalonia.Threading;
using ReactiveUI;
using System.Reactive;
using HexClientProject.StateManagers;

namespace HexClientProject.ViewModels;

public class MatchFoundViewModel : ReactiveObject
{
    private readonly GlobalStateManager _globalStateManager = GlobalStateManager.Instance;

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
        private set => this.RaiseAndSetIfChanged(ref _isActionDone, value);
    }

    public Action? CloseMatchFoundPopUpRequest { get; set; }
    public Action? OnRefuseOrTimeoutMatchRequested { get; init; }
    public Action? OnAcceptMatchRequested { get; init; }
    public Action? OnCancelMatchRequested { get; init; }

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

        if (_globalStateManager.IsOnlineMode)
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

        OnRefuseOrTimeoutMatchRequested?.Invoke();
        CloseMatchFoundPopUpRequest?.Invoke();
    }
}
