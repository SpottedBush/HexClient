using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace HexClientProject.Converters;

public class InverseBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return !b;
        return true;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return !b;
        return false;
    }

    public InverseBoolConverter ProvideValue(IServiceProvider serviceProvider) => this;
}