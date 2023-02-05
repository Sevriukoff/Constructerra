using System.Windows.Controls;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class WelcomeView : INavigableView<WelcomeViewModel>
{
    public WelcomeView()
    {
        InitializeComponent();
    }

    public WelcomeViewModel ViewModel { get; }
}