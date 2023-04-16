using System;
using System.Drawing;
using System.Globalization;
using System.Reactive;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace TerrariaConstructor.Common.Converters;

public class SelectedIndexToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (int.TryParse(value.ToString(), out int selectedIndex) && int.TryParse(parameter.ToString(), out int index))
        {
            return selectedIndex == index
                ? new SolidColorBrush(Wpf.Ui.Appearance.Accent.SecondaryAccent)
                : new SolidColorBrush(Color.FromRgb(234, 237, 237));
        }

        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}