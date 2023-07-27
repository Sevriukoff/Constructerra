using System.Windows.Controls;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class ResearchView : Page, INavigableView<ResearchViewModel>
{
    public ResearchView(ResearchViewModel viewModel)
    {
        InitializeComponent();
        
        ViewModel = viewModel;
        DataContext = ViewModel;
    }

    public ResearchViewModel ViewModel { get; }
}