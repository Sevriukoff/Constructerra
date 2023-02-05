using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TerrariaConstructor.Common.Converters;

public class BooleanToBrushConverter : IValueConverter
{
    public Brush TrueValue { get; set; } = new SolidColorBrush(Colors.Aquamarine);
    public Brush FalseValue { get; set; } = new SolidColorBrush(Colors.Bisque);
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? TrueValue : FalseValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}