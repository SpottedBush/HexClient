using HexClientProject.StateManagers;
using HexClientProject.ViewModels.SideBar;

namespace HexClientProject.ViewModels.DraftPhase;

public class DraftViewModel
{
    private readonly GlobalStateManager _globalStateManager = GlobalStateManager.Instance;
    private readonly SocialStateManager _socialStateManager = SocialStateManager.Instance;
    public DraftViewModel()
    {
        _globalStateManager.IsInDraft = true;
        ChatBoxViewModel? chatVm = _socialStateManager.ChatBoxViewModel;
    }
}