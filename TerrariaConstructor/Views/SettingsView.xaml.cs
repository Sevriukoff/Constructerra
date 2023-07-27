using System.Windows.Controls;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class SettingsView : INavigableView<SettingsViewModel>
{
    public SettingsView(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        
        InitializeComponent();
    }

    public SettingsViewModel ViewModel { get; }
}