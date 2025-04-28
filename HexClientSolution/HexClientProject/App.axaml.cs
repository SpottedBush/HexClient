using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HexClientProject.ViewModels;
using HexClientProject.Views;
using HexClientProject.Views.ViewManagement;

namespace HexClientProject
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindowView();
            }

#if DEBUG
            this.AttachDevTools();
#endif
            base.OnFrameworkInitializationCompleted();
        }
    }
}