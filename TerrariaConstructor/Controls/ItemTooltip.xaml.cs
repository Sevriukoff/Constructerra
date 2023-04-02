using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Controls;

public partial class ItemTooltip : UserControl
{
    public static readonly DependencyProperty ItemProperty = 
        DependencyProperty.Register(nameof(Item),
            typeof(Item), typeof(ItemTooltip), new PropertyMetadata(null));

    public Item Item
    {
        get => (Item) GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register(
        nameof(ItemName), typeof(string), typeof(ItemTooltip), new PropertyMetadata(default(string)));

    public string ItemName
    {
        get => (string) GetValue(ItemNameProperty);
        set => SetValue(ItemNameProperty, value);
    }

    public ItemTooltip()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        
    }
    
}