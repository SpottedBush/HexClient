using Avalonia.Controls;
using HexClientProject.Models;
using ReactiveUI;

namespace HexClientProject.StateManagers;

public class ViewStateManager : ReactiveObject
{
    private static ViewStateManager? _instance;

    // Public property to access the single instance
    public static ViewStateManager Instance
    {
        get
        {
            _instance ??= new ViewStateManager();
            return _instance;
        }
    }
    private UserControl _leftPanelContent = null!;
    public UserControl LeftPanelContent
    {
        get => _leftPanelContent;
        set => this.RaiseAndSetIfChanged(ref _leftPanelContent, value);
    }
        
    private UserControl _currView = null!;
    public UserControl CurrView
    {
        get => _currView;
        set => this.RaiseAndSetIfChanged(ref _currView, value);
    }
}