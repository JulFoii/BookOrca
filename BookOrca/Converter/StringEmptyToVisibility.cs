using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookOrca.Converter;

internal class StringEmptyToVisibility : IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isInverted = parameter?.ToString()?.ToLower().Contains("inverted") ?? false;

        var str = (string?)value;

        if (string.IsNullOrEmpty(str)) return GetReturnValue(isInverted, Visibility.Visible);

        return GetReturnValue(isInverted, Visibility.Hidden);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new Exception("Can't convert back");
    }

    private static Visibility GetReturnValue(bool isInverted, Visibility value)
    {
        if (!isInverted) return value;

        return value == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
    }
}