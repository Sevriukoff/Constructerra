using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TerrariaConstructor.Common.Converters;

public class IntToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int.TryParse(parameter?.ToString(), out var minValue);
        
        if (value is int intValue && intValue <= minValue)
        {
            return Visibility.Collapsed;
        }

        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}