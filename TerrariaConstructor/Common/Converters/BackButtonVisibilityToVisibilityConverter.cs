using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Common.Converters;

public class BackButtonVisibilityToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Converts <see cref="SolidColorBrush"/> to <see langword="Color"/>.
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is not NavigationViewBackButtonVisible backButtonVisibility)
            return Visibility.Collapsed;

        switch (backButtonVisibility)
        {
            case NavigationViewBackButtonVisible.Collapsed:
                return Visibility.Collapsed;
            default:
                return Visibility.Visible;
        }
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