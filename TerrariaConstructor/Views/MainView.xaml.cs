using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Splat;
using TerrariaConstructor.Models;
using TerrariaConstructor.Services;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls.Navigation;
using Wpf.Ui.Services;

namespace TerrariaConstructor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INavigationWindow
    {
        public MainWindow(INavigationService navigationService)
        {
            InitializeComponent();

            var pageService = new PageService();

            SetPageService(pageService);
            navigationService.SetNavigationControl(RootNavigation);

            RootNavigation.Loaded += (_, _) => RootNavigation.Navigate(typeof(WelcomeView));
        }

        public INavigationView GetNavigation()
        {
            return RootNavigation;
        }

        public bool Navigate(Type pageType)
        {
            return RootNavigation.Navigate(pageType);
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        public void SetPageService(IPageService pageService)
        {
            RootNavigation.SetPageService(pageService);
        }

        public void ShowWindow()
        {
            Show();
        }

        public void CloseWindow()
        {
            Close();
        }
    }
}