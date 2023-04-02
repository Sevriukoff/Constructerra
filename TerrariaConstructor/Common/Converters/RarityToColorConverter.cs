using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Common.Converters;

public class RarityToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string color = "#8263FF";
        
        if (value is ItemRarity rarity)
        {
            color = rarity switch
            {
                ItemRarity.None => "#818181",
                ItemRarity.Gray => "#818181",
                ItemRarity.White => "#FFF",
                ItemRarity.Blue => "#9696FF",
                ItemRarity.Green => "#8EF28E",
                ItemRarity.Orange => "#FFC896",
                ItemRarity.LightRed => "#FF9696",
                ItemRarity.Pink => "#FF96FF",
                ItemRarity.LightPurple => "#D2A0FF",
                ItemRarity.Lime => "#96FF0A",
                ItemRarity.Yellow => "#FFFF0A",
                ItemRarity.Cyan => "#05C7FE",
                ItemRarity.Red => "#FF2864",
                ItemRarity.Purple => "#B428FF",
                ItemRarity.FieryRed => "#FF8C00",
                ItemRarity.Amber => "#FFAF00",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)!);
        
        return brush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}