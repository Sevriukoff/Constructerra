using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace TerrariaConstructor.Common.Converters;

public class ByteToDifficultyConverter : IValueConverter
{
    private List<string> Difficulties => new List<string>
    {
        "Классика", "Средняя сложность", "Сложный режим", "Путешествие"
    };
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is byte index)
        {
            return Difficulties[index];
        }

        return "Неизвестно";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}