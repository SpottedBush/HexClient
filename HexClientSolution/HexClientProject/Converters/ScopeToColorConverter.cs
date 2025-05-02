using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;
using HexClientProject.Models;

namespace HexClientProject.Converters;
public class ScopeToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            string s => s switch
            {
                "Global" => Brushes.Cyan,
                "Party" => Brushes.Green,
                "Whisper" => Brushes.MediumPurple,
                "Guild" => Brushes.Lime,
                "Draft" => Brushes.Orange,
                "System" => Brushes.Red,
                _ => Brushes.MediumPurple // Default, corresponds to conversations with friends
            },
            ChatScope scope => scope switch
            {
                ChatScope.Global => Brushes.Cyan,
                ChatScope.Party => Brushes.Green,
                ChatScope.Whisper => Brushes.MediumPurple,
                ChatScope.Guild => Brushes.Lime,
                ChatScope.Draft => Brushes.Orange,
                ChatScope.System => Brushes.Red,
                _ => Brushes.White
            },
            _ => Brushes.White
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}