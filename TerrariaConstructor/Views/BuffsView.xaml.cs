using System.Windows.Controls;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class BuffsView : INavigableView<BuffsViewModel>
{
    public BuffsView(BuffsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        
        InitializeComponent();
    }

    public BuffsViewModel ViewModel { get; }
}