using HexClientProject.Models;

namespace HexClientProject.Converters;

using Avalonia.Data.Converters;
using Avalonia;
using System;
using System.Globalization;

public class SenderToPaddingConverter : IValueConverter
{
    StateManager _stateManager = StateManager.Instance;
    public Thickness LeftPadding { get; set; } = new Thickness(100, 5, 10, 5); // Friend
    public Thickness RightPadding { get; set; } = new Thickness(10, 5, 100, 5); // You

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string sender)
        {
            return sender == _stateManager.SummonerInfo.GameName ? RightPadding : LeftPadding;
        }

        return new Thickness(10);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
