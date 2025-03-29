using System;
using CommunityToolkit.Mvvm.Input;
using HexClientProject.Models;

namespace HexClientProject.ViewModels;

public partial class GameModeSelectionViewModel : ViewModelBase
{
    public event Action? RequestLobbyView; // Event to notify MainViewModel

    [RelayCommand]
    public void SwitchToLobby()
    {
        RequestLobbyView?.Invoke(); // Trigger event
        LobbyInfo.SetLobbyInfo();
    }
}