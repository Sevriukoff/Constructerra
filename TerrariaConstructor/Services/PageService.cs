using System;
using System.Windows;
using Autofac;
using Wpf.Ui.Contracts;

namespace TerrariaConstructor.Services;

public class PageService : IPageService
{
    public T GetPage<T>() where T : class
    {
        if (!typeof(FrameworkElement).IsAssignableFrom(typeof(T)))
            throw new InvalidOperationException("The page should be a WPF control.");
        
        return (T)App.Container.Resolve(typeof(T));
        
        //return (T?)_serviceProvider.GetService(typeof(T));
    }

    public FrameworkElement GetPage(Type pageType)
    {
        if (!typeof(FrameworkElement).IsAssignableFrom(pageType))
            throw new InvalidOperationException("The page should be a WPF control.");

        return App.Container.Resolve(pageType) as FrameworkElement;
        
        //return _serviceProvider.GetService(pageType) as FrameworkElement;
    }
}