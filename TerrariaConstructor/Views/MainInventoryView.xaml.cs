using System.Windows.Controls;
using ReactiveUI;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class MainInventoryView : INavigableView<ReactiveObject>
{
    public MainInventoryView()
    {
        //DataContext = ViewModel;
        
        InitializeComponent();
    }

    public ReactiveObject ViewModel { get; }
}