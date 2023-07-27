using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Autofac;
using Splat;
using TerrariaConstructor.Models;
using TerrariaConstructor.Services;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls.Navigation;
using Wpf.Ui.Services;
using INavigationService = TerrariaConstructor.Services.INavigationService;

namespace TerrariaConstructor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INavigationWindow
    {
        public MainWindow(INavigationService navigationService, ISnackbarService snackbarService,
            IDialogService dialogService)
        {
            InitializeComponent();

            var pageService = new PageService();

            SetPageService(pageService);
            navigationService.SetNavigationControl(RootNavigation);
            snackbarService.SetSnackbarControl(RootSnackbar);
            dialogService.SetDialogControl(RootDialog);

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

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 1000)
            {
                RootNavigation.PaneDisplayMode = NavigationViewPaneDisplayMode.LeftFluent;
            }
            else
            {
                 RootNavigation.PaneDisplayMode = NavigationViewPaneDisplayMode.LeftMinimal;
            }
        }

        private void RootNavigation_OnPaneOpened(object sender, RoutedEventArgs e)
        {
            RootNavigation.PaneDisplayMode = RootNavigation.PaneDisplayMode == NavigationViewPaneDisplayMode.LeftFluent
                ? NavigationViewPaneDisplayMode.LeftMinimal
                : NavigationViewPaneDisplayMode.LeftFluent;
        }

        private void RootNavigation_OnLoaded(object sender, RoutedEventArgs e)
        {
            // var navView = sender as NavigationView;
            // var rootGrid = VisualTreeHelper.GetChild(navView, 0) as Grid;
            //
            // var paneToggleButtonGrid = VisualTreeHelper.GetChild(rootGrid, 0) as Grid;
            // var border = VisualTreeHelper.GetChild(paneToggleButtonGrid, 0) as Border;
            // var firstGrid = VisualTreeHelper.GetChild(border, 0) as  Grid;
            // var secondGrid = VisualTreeHelper.GetChild(firstGrid, 0) as Grid;
            // var toggleButton = VisualTreeHelper.GetChild(secondGrid, 0) as Button;
            //
            // toggleButton.Click += RootNavigation_OnPaneOpened;
        }
    }
}