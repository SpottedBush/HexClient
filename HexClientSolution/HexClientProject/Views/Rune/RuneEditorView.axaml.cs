using System;
using Avalonia.Controls;
using Avalonia.Input;
using HexClientProject.ViewModels.RuneSystem;
using HexClientProject.ViewModels.RuneSystem.RuneEditor;

namespace HexClientProject.Views.Rune;

public partial class RuneEditorView : UserControl
{
    public RuneEditorViewModel ViewModel => (RuneEditorViewModel)DataContext!;
    public RuneEditorView()
    {
        InitializeComponent();
    }
    
    private void OnRenameEnter(object? sender, KeyEventArgs e)
    {
        var textbox = sender as TextBox;
        if (textbox == null) return;
        if ((e.Key != Key.Tab && e.Key != Key.Enter && e.Key != Key.Escape) || DataContext is not RuneEditorViewModel vm) return;
        if (e.Key == Key.Escape) vm.CancelRenameCommand.Execute().Subscribe();
        vm.ConfirmRenameCommand.Execute().Subscribe();
        e.Handled = true;
    }
}