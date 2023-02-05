using System.Windows.Controls;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class MainInventoryView : INavigableView<MainInventoryViewModel>
{
    public MainInventoryView(MainInventoryViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        
        InitializeComponent();
    }

    public MainInventoryViewModel ViewModel { get; }
}