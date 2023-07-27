using System.Windows.Controls;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class PlayersView : INavigableView<PlayersViewModel>
{
    public PlayersView(PlayersViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        
        InitializeComponent();
    }

    public PlayersViewModel ViewModel { get; }
}