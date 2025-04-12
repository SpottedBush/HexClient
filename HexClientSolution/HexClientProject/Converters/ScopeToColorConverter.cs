using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;
using HexClientProject.Models;

namespace HexClientProject.Converters;
public class ScopeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ChatScope scope)
        {
            return scope switch
            {
                ChatScope.Global => Brushes.Cyan,
                ChatScope.Party => Brushes.Green,
                ChatScope.Whisper => Brushes.MediumPurple,
                ChatScope.Guild => Brushes.Lime,
                _ => Brushes.White
            };
        }
        return Brushes.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}