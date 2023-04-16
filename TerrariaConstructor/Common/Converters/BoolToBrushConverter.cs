using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TerrariaConstructor.Common.Converters;

public class BoolToBrushConverter : IValueConverter
{
    public Brush TrueValue { get; set; } = new SolidColorBrush(Wpf.Ui.Appearance.Accent.SecondaryAccent){Opacity = 0.6};
    public Brush FalseValue { get; set; } = new SolidColorBrush(Color.FromRgb(234,237,237)){Opacity = 0.9};
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? TrueValue : FalseValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}