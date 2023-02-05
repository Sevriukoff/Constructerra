using System;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Services;

public class NavigationPageService : INavigationService
{
    /// <summary>Locally attached service provider.</summary>
    //private readonly IServiceProvider _serviceProvider;

    /// <summary>Locally attached page service.</summary>
    private IPageService _pageService;

    /// <summary>Control representing navigation.</summary>
    protected INavigationView NavigationControl;

    public NavigationPageService()
    {
        
    }

    /// <inheritdoc />
    public INavigationView GetNavigationControl() => this.NavigationControl;

    /// <inheritdoc />
    public void SetNavigationControl(INavigationView navigation)
    {
        this.NavigationControl = navigation;
        if (this._pageService != null)
            this.NavigationControl.SetPageService(this._pageService);
    }

    /// <inheritdoc />
    public void SetPageService(IPageService pageService)
    {
        if (this.NavigationControl == null)
            this._pageService = pageService;
        else
            this.NavigationControl.SetPageService(this._pageService);
    }

    /// <inheritdoc />
    public bool Navigate(Type pageType) => this.NavigationControl != null && this.NavigationControl.Navigate(pageType);

    /// <inheritdoc />
    public bool Navigate(string pageTag) => this.NavigationControl != null && this.NavigationControl.Navigate(pageTag);
}