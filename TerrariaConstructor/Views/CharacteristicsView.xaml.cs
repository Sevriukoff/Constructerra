using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.Views;

public partial class CharacteristicsView : INavigableView<CharacteristicsViewModel>, IViewFor<CharacteristicsViewModel>
{
    private readonly AppSettings _appSettings;

    public CharacteristicsView(CharacteristicsViewModel model, AppSettings appSettings)
    {
        _appSettings = appSettings;
        _appSettings.LoadSettings();
        InitializeComponent();
        
        AddTextBoxFocusEvent(NameTextBox, IconNameTextBox);
        NameTextBox.Focus();
        AddTextBoxFocusEvent(HealthTextBox, IconHealthTextBox);
        AddTextBoxFocusEvent(MaxHealthTextBox, IconHealthTextBox);
        AddTextBoxFocusEvent(ManaTextBox, IconManaTextBox);
        AddTextBoxFocusEvent(MaxManaTextBox, IconManaTextBox);
        AddTextBoxFocusEvent(AnglerQuestsTextBox, IconAnglerQuestsTextBox);
        AddTextBoxFocusEvent(GolferScoreTextBox, IconGolferScoreTextBox);
        AddTextBoxFocusEvent(PveTextBox, IconPveTextBox);
        AddTextBoxFocusEvent(PvpTextBox, IconPvpTextBox);
        //AddTextBoxFocusEvent(PlayTimeTextBox, IconPlayTimeTextBox);

        ViewModel = model;
        DataContext = ViewModel;
    }
    
    private void AddTextBoxFocusEvent(TextBox textBox, UIElement icon)
    {
        textBox.GotFocus += (_, _) =>
        {
            if (!_appSettings.ShowTooltips)
                return;
            
            icon.Visibility = Visibility.Visible;
        };
        
        textBox.LostFocus += (_, _) =>
        {
            if (!_appSettings.ShowTooltips)
                return;;

            icon.Visibility = Visibility.Collapsed;
        };
    }

    public CharacteristicsViewModel ViewModel { get; set; }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (CharacteristicsViewModel)value;
    }
}