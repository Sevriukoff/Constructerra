using System.Diagnostics;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Controls;

public class InventoryCellPresenter : ContentControl
{
    public static readonly DependencyProperty WikiUrlProperty = DependencyProperty.Register(
        nameof(WikiUrl), typeof(string), typeof(InventoryCellPresenter), new PropertyMetadata(default(string)));

    public string WikiUrl
    {
        get => (string) GetValue(WikiUrlProperty);
        set => SetValue(WikiUrlProperty, value);
    }

    public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
        nameof(Image), typeof(ImageSource), typeof(InventoryCellPresenter), new PropertyMetadata(default(ImageSource)));

    public ImageSource Image
    {
        get => (ImageSource) GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public static readonly DependencyProperty IsFavoriteProperty = DependencyProperty.Register(
        nameof(IsFavorite), typeof(bool), typeof(InventoryCellPresenter), new PropertyMetadata(false));

    public bool IsFavorite
    {
        get => (bool) GetValue(IsFavoriteProperty);
        set => SetValue(IsFavoriteProperty, value);
    }
    
    public static readonly DependencyProperty TooltipContentProperty = DependencyProperty.Register(
        nameof(TooltipContent), typeof(object), typeof(InventoryCellPresenter), new PropertyMetadata(default(object)));

    public object TooltipContent
    {
        get => (object) GetValue(TooltipContentProperty);
        set => SetValue(TooltipContentProperty, value);
    }

    public static readonly DependencyProperty FooterContentProperty = DependencyProperty.Register(
        nameof(FooterContent), typeof(object), typeof(InventoryCellPresenter), new PropertyMetadata(default(object)));

    public object FooterContent
    {
        get => (object) GetValue(FooterContentProperty);
        set => SetValue(FooterContentProperty, value);
    }

    public static readonly DependencyProperty ItemIdProperty = DependencyProperty.Register(
        nameof(ItemId), typeof(int), typeof(InventoryCellPresenter), new PropertyMetadata(0));

    public int ItemId
    {
        get => (int) GetValue(ItemIdProperty);
        set => SetValue(ItemIdProperty, value);
    }
    
    public static readonly DependencyProperty ItemClickCommandProperty = DependencyProperty.Register(
        nameof(ItemClickCommand), typeof(ReactiveCommand<Unit, Unit>), typeof(InventoryCellPresenter), new PropertyMetadata(default(ReactiveCommand<Unit, Unit>)));

    public ReactiveCommand<Unit, Unit> ItemClickCommand => (ReactiveCommand<Unit, Unit>) GetValue(ItemClickCommandProperty);

    public InventoryCellPresenter()
    {
        SetValue(ItemClickCommandProperty,
            ReactiveCommand.Create<Unit>(ItemClick));
    }

    private void ItemClick(Unit parameter)
    {
        if (!string.IsNullOrEmpty(WikiUrl))
        {
            var psi = new ProcessStartInfo
            {
                FileName = WikiUrl,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}