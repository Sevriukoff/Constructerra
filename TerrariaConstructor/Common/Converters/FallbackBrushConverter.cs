using System;
using System.Windows.Data;
using System.Windows.Media;

namespace TerrariaConstructor.Common.Converters;

public class FallbackBrushConverter : IValueConverter
{
    /// <summary>
    /// Converts <see cref="SolidColorBrush"/>  to <see langword="Color"/>.
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
            return brush;

        if (value is Color)
            return new SolidColorBrush((Color)value);

        // We draw red to visibly see an invalid bind in the UI.
        return new SolidColorBrush(new Color { A = 255, R = 255, G = 0, B = 0 });
    }

    /// <summary>
    /// Not Implemented.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}