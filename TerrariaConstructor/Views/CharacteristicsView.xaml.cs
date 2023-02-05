using System.Windows.Controls;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class CharacteristicsView : INavigableView<CharacteristicsViewModel>
{
    public CharacteristicsView(CharacteristicsViewModel model)
    {
        ViewModel = model;
        DataContext = ViewModel;
        
        InitializeComponent();
    }

    public CharacteristicsViewModel ViewModel { get; }
}