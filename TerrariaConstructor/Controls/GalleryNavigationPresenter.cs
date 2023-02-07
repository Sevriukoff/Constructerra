using System;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using ReactiveUI;
using TerrariaConstructor.Services;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Contracts;
using Wpf.Ui.Services;
using INavigationService = TerrariaConstructor.Services.INavigationService;

namespace TerrariaConstructor.Controls;

public class GalleryNavigationPresenter : Control
{
    /// <summary>
    /// Property for <see cref="ItemsSource"/>.
    /// </summary>
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource),
            typeof(object), typeof(GalleryNavigationPresenter), new PropertyMetadata(null));

    /// <summary>
    /// Property for <see cref="TemplateButtonCommand"/>.
    /// </summary>
    public static readonly DependencyProperty TemplateButtonCommandProperty =
        DependencyProperty.Register(nameof(TemplateButtonCommand),
            typeof(ReactiveCommand<string, Unit>), typeof(GalleryNavigationPresenter), new PropertyMetadata(null));

    public object ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Command triggered after clicking the titlebar button.
    /// </summary>
    public ReactiveCommand<string, Unit> TemplateButtonCommand => (ReactiveCommand<string, Unit>)GetValue(TemplateButtonCommandProperty);

    /// <summary>
    /// Creates a new instance of the class and sets the default <see cref="FrameworkElement.Loaded"/> event.
    /// </summary>
    public GalleryNavigationPresenter()
    {
        SetValue(TemplateButtonCommandProperty,
            ReactiveCommand.Create<string>(o => OnTemplateButtonClick(o ?? String.Empty)));
    }

    private void OnTemplateButtonClick(string parameter)
    {
        var navigationService = App.Container.Resolve<INavigationService>();

        if (navigationService == null)
            return;

        var pageType = NameToPageTypeService.Convert("MainInventory");
        var viewModelType = NameToViewModelTypeService.Convert(parameter);

        if (pageType != null)
            navigationService.NavigateWithDataContext(pageType, (ReactiveObject)App.Container.Resolve(viewModelType));

#if DEBUG
        System.Diagnostics.Debug.WriteLine(
            $"INFO | {nameof(GalleryNavigationPresenter)} navigated, {parameter} ({pageType})", "Wpf.Ui.Gallery");
#endif
    }
}