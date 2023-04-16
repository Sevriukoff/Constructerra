using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TerrariaConstructor.Common.Converters;

public class FavoriteToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isFavorite)
        {
            var activeColor = (Color) ColorConverter.ConvertFromString("#FFD3AA");
            var inactiveColor = Colors.LightGray;
            
            if (parameter is string colorType && colorType == "background")
            {
                activeColor.A = 25;
                inactiveColor = Colors.Transparent;
            }

            return isFavorite ? new SolidColorBrush(activeColor) : new SolidColorBrush(inactiveColor);
        }

        throw new AggregateException("value is not a boolean");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}