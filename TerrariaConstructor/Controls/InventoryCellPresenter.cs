using System.Windows;
using System.Windows.Controls;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Controls;

public class InventoryCellPresenter : Control
{
    public static readonly DependencyProperty InventoryItemProperty = DependencyProperty.Register(
        nameof(InventoryItem), typeof(Item), typeof(InventoryCellPresenter), new PropertyMetadata(default(Item)));

    public Item InventoryItem
    {
        get => (Item) GetValue(InventoryItemProperty);
        set => SetValue(InventoryItemProperty, value);
    }
}