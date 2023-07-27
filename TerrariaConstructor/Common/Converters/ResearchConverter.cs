using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Common.Converters;

public class ResearchConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Item item)
        {
            return item.QuantityForResearch - item.Stack == 0;
        }

        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}