using System;
using System.Globalization;
using System.IO.Compression;
using System.Windows;
using System.Windows.Data;

namespace TerrariaConstructor.Common.Converters;

public class DifferenceConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 2 && values[0] is int quantity && values[1] is int stack)
        {
            int difference = quantity - stack;
            
            if (targetType == typeof(string))
                return $"Исследуйте ещё {difference}, чтобы разблокировать дублирование";
            
            return difference == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        
        return null;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}