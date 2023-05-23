using System;
using System.Linq;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Controls;

public class ItemTooltipPresenter : Control
{
    public static readonly DependencyProperty ItemProperty = 
        DependencyProperty.Register(nameof(Item),
            typeof(Item), typeof(ItemTooltipPresenter), new PropertyMetadata(null));

    public Item Item
    {
        get => (Item) GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public ItemTooltipPresenter()
    {
        SetValue(TemplateButtonCommandProperty,
            ReactiveCommand.Create<string>(o => OnTemplateButtonClick(o ?? String.Empty)));
    }

    public static readonly DependencyProperty TemplateButtonCommandProperty =
        DependencyProperty.Register(nameof(TemplateButtonCommand),
            typeof(ReactiveCommand<string, Unit>), typeof(ItemTooltipPresenter), new PropertyMetadata(null));
    
    public ReactiveCommand<string, Unit> TemplateButtonCommand => (ReactiveCommand<string, Unit>)GetValue(TemplateButtonCommandProperty);
    
    private void OnTemplateButtonClick(string parameter)
    {
        
    }

    public bool IsMaterial => Item.Categories.Contains("Crafting material");
    public bool IsConsumable => Item.Categories.Contains("Consumable");

    public bool IsNothingToShow => !IsMaterial && !IsConsumable && Item.Damage == 0 && Item.DefensePoint == 0 &&
                                   !Item.IsPlaceable && string.IsNullOrEmpty(Item.Tooltip);
}