using System;
using Avalonia.Controls;
using Avalonia.Input;
using HexClientProject.ViewModels.RuneSystem;

namespace HexClientProject.Views.Rune;

public partial class RuneEditorView : UserControl
{
    public RuneEditorView()
    {
        InitializeComponent();
    }
    
    private void OnRenameEnter(object? sender, KeyEventArgs e)
    {
            
        var textbox = sender as TextBox;
        if (textbox == null) return;
        if ((e.Key != Key.Tab && e.Key != Key.Enter) || DataContext is not RuneEditorViewModel vm) return;
        vm.ConfirmRenameCommand.Execute().Subscribe();
        e.Handled = true;
    }
}