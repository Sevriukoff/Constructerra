using System.Windows.Controls;
using ReactiveUI;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class EquipsView : INavigableView<EquipsViewModel>, IViewFor<EquipsViewModel>
{
    public EquipsView(EquipsViewModel viewModel)
    {
        InitializeComponent();
        
        ViewModel = viewModel;
        DataContext = ViewModel;
    }

    public EquipsViewModel ViewModel { get; set; }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (EquipsViewModel)value;
    }
}