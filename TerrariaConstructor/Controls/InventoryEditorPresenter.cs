using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Controls;

public class InventoryEditorPresenter : Control
{
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
        nameof(SelectedItem), typeof(Item), typeof(InventoryEditorPresenter), new PropertyMetadata(default(Item)));

    public Item SelectedItem
    {
        get { return (Item) GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public static readonly DependencyProperty DeleteSelectedItemCommandProperty = DependencyProperty.Register(
        nameof(DeleteSelectedItemCommand), typeof(ReactiveCommand<Unit, Unit>), typeof(InventoryEditorPresenter), new PropertyMetadata(default(ReactiveCommand<Unit, Unit>)));

    public ReactiveCommand<Unit, Unit> DeleteSelectedItemCommand => (ReactiveCommand<Unit, Unit>) GetValue(DeleteSelectedItemCommandProperty);

    public InventoryEditorPresenter()
    {
        SetValue(DeleteSelectedItemCommandProperty, 
            ReactiveCommand.Create( () =>
            {
                SelectedItem.Id = 0;
                SelectedItem.Name = null;
                SelectedItem.InternalName = null;
                SelectedItem.Image = null;
                SelectedItem.Description = null;
                SelectedItem.Tooltip = null;
                SelectedItem.Rarity = default(ItemRarity);
                SelectedItem.Categories = null;
                SelectedItem.Damage = 0;
                SelectedItem.DamageType = default(DamageType);
                SelectedItem.CriticalChance = 0;
                SelectedItem.Knockback = 0.0;
                SelectedItem.KnockbackType = default(KnockbackType);
                SelectedItem.UseTime = 0;
                SelectedItem.PickaxePower = 0;
                SelectedItem.AxePower = 0;
                SelectedItem.HammerPower = 0;
                SelectedItem.DefensePoint = 0;
                SelectedItem.QuantityForResearch = 0;
                SelectedItem.CostByBuy = 0;
                SelectedItem.CostBySell = 0;
                SelectedItem.Prefix = 0;
                SelectedItem.Modifier = default(Modifier);
                SelectedItem.Stack = 0;
                SelectedItem.MaxStack = 0;
                SelectedItem.IsPlaceable = false;
                SelectedItem.IsFavorite = false;
                SelectedItem.WikiUrl = null;
            }));
    }

    public bool IsItemMode { get; set; }

    public bool IsResearchMode => !IsItemMode;
}