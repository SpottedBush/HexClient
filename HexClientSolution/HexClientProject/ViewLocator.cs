using Avalonia.Controls;
using Avalonia.Controls.Templates;
using HexClientProject.ViewModels;
using System;

namespace HexClientProject
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object? data)
        {
            var name = data?.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name!);

            return type != null ? (Control)Activator.CreateInstance(type)! : new TextBlock { Text = "View Not Found" };
        }

        public bool Match(object? data) => data is ViewModelBase;
    }
}