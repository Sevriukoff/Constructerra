using System;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Controls;

public class ImageTooltipPresenter : Control
{
    public static readonly DependencyProperty ItemProperty = 
        DependencyProperty.Register(nameof(Item),
            typeof(Item), typeof(ImageTooltipPresenter), new PropertyMetadata(null));

    public Item Item
    {
        get => (Item) GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public ImageTooltipPresenter()
    {
        SetValue(TemplateButtonCommandProperty,
            ReactiveCommand.Create<string>(o => OnTemplateButtonClick(o ?? String.Empty)));
    }

    public static readonly DependencyProperty TemplateButtonCommandProperty =
        DependencyProperty.Register(nameof(TemplateButtonCommand),
            typeof(ReactiveCommand<string, Unit>), typeof(ImageTooltipPresenter), new PropertyMetadata(null));
    
    public ReactiveCommand<string, Unit> TemplateButtonCommand => (ReactiveCommand<string, Unit>)GetValue(TemplateButtonCommandProperty);
    
    private void OnTemplateButtonClick(string parameter)
    {
        
    }
}