using System.Windows.Controls;
using TerrariaConstructor.ViewModels;
using TerrariaConstructor.ViewModels.Inventories;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class InventoriesView : INavigableView<InventoriesViewModel>
{
    public InventoriesView(InventoriesViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        
        InitializeComponent();
    }

    public InventoriesViewModel ViewModel { get; }
}