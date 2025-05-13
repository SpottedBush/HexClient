using HexClientProject.Models;

namespace HexClientProject.Interfaces;

public interface ILobbyService
{
    /// <summary>
    /// Creates and initializes a new instance of the LobbyInfoModel class, thus creating a new lobby.
    /// The instance contains information about the current lobby, including
    /// properties such as lobby name, lobby password, players, maximum player limit,
    /// and the currently selected game mode.
    /// </summary>
    /// <returns>
    /// An instance of LobbyInfoModel containing the lobby's configuration and participant details.
    /// </returns>
    LobbyInfoModel CreateLobbyInfoModel();
}