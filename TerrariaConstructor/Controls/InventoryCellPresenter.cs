using System.Diagnostics;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
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

    public static readonly DependencyProperty ItemClickCommandProperty = DependencyProperty.Register(
        nameof(ItemClickCommand), typeof(ReactiveCommand<Unit, Unit>), typeof(InventoryCellPresenter), new PropertyMetadata(default(ReactiveCommand<Unit, Unit>)));

    public ReactiveCommand<Unit, Unit> ItemClickCommand => (ReactiveCommand<Unit, Unit>) GetValue(ItemClickCommandProperty);

    public InventoryCellPresenter()
    {
        SetValue(ItemClickCommandProperty,
            ReactiveCommand.Create<Unit, Unit>(ItemClick));
    }

    private Unit ItemClick(Unit parameter)
    {
        if (InventoryItem.Id > 0)
        {
            var psi = new ProcessStartInfo
            {
                FileName = InventoryItem.WikiUrl,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        return Unit.Default;
    }
}