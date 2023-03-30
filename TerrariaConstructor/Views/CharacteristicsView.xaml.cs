using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class CharacteristicsView : INavigableView<CharacteristicsViewModel>, IViewFor<CharacteristicsViewModel>
{
    public CharacteristicsView(CharacteristicsViewModel model)
    {
        InitializeComponent();

        NameTextBox.Focus();

        AddTextBoxFocusEvent(NameTextBox, IconNameTextBox);
        AddTextBoxFocusEvent(HealthTextBox, IconHealthTextBox);
        AddTextBoxFocusEvent(MaxHealthTextBox, IconHealthTextBox);
        AddTextBoxFocusEvent(ManaTextBox, IconManaTextBox);
        AddTextBoxFocusEvent(MaxManaTextBox, IconManaTextBox);
        AddTextBoxFocusEvent(AnglerQuestsTextBox, IconAnglerQuestsTextBox);
        AddTextBoxFocusEvent(GolferScoreTextBox, IconGolferScoreTextBox);
        AddTextBoxFocusEvent(PveTextBox, IconPveTextBox);
        AddTextBoxFocusEvent(PvpTextBox, IconPvpTextBox);
        AddTextBoxFocusEvent(PlayTimeTextBox, IconPlayTimeTextBox);
        
        ViewModel = model;
        DataContext = ViewModel;
    }
    
    private void AddTextBoxFocusEvent(TextBox textBox, UIElement icon)
    {
        textBox.GotFocus += (_, _) => icon.Visibility = Visibility.Visible;
        textBox.LostFocus += (_, _) => icon.Visibility = Visibility.Collapsed;
    }

    public CharacteristicsViewModel ViewModel { get; set; }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (CharacteristicsViewModel)value;
    }
}