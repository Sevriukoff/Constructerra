using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Models;
using TerrariaConstructor.Views;
using Wpf.Ui.Common;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.ViewModels;

public class MainViewModel : ReactiveObject
{
    private ObservableCollection<object> _navigationItems;
    public PlayerModel PlayerModel { get; }
    public CharacteristicsViewModel CharacteristicsViewModel { get; set; }
    public EquipsViewModel EquipsViewModel { get; set; }

    public ObservableCollection<object> NavigationItems
    {
        get => _navigationItems;
        set => this.RaiseAndSetIfChanged(ref _navigationItems, value);
    }

    public InventoriesViewModel InventoriesViewModel { get; set; }

    public string Title => "ConstrucTerra - создавай своих героев";

    public MainViewModel(PlayerModel playerModel,
        CharacteristicsViewModel characteristicsViewModel,
        EquipsViewModel equipsViewModel,
        InventoriesViewModel inventoriesViewModel)
    {
        PlayerModel = playerModel;
        CharacteristicsViewModel = characteristicsViewModel;
        EquipsViewModel = equipsViewModel;
        InventoriesViewModel = inventoriesViewModel;

        NavigationItems = new ObservableCollection<object>
        {
            new NavigationViewItem
            {
                Content = "Приветсвие",
                Icon = new SymbolIcon{Symbol = SymbolRegular.Star12},
                TargetPageType = typeof(WelcomeView)
            },
            new NavigationViewItem
            {
                Content = "Характеристика",
                Icon = new SymbolIcon{Symbol = SymbolRegular.Heart12},
                TargetPageType = typeof(CharacteristicsView)
            },
            new NavigationViewItem
            {
                Content = "Экипировка",
                Icon = new SymbolIcon{Symbol = SymbolRegular.Toolbox12},
                TargetPageType = typeof(EquipsView)
            },
            new NavigationViewItem
            {
                Content = "Инструменты",
                Icon = new SymbolIcon{Symbol = SymbolRegular.Ruler16},
                TargetPageType = typeof(ToolsView)
            },
            new NavigationViewItem
            {
                Content = "Инвентарь",
                Icon = new SymbolIcon{Symbol = SymbolRegular.Savings16},
                TargetPageType = typeof(InventoriesView)
            },
            new NavigationViewItem
            {
                Content = "Баффы",
                Icon = new SymbolIcon{Symbol = SymbolRegular.Lightbulb20},
                TargetPageType = typeof(BuffsView)
            },
            new NavigationViewItem
            {
                Content = "Изучение",
                Icon = new SymbolIcon{Symbol = SymbolRegular.BrainCircuit20},
                TargetPageType = typeof(ResearchView)
            }
        };

        PlayerModel.PlayerUpdated += () =>
        {
            CharacteristicsViewModel.Update();
            EquipsViewModel.Update();
            InventoriesViewModel.Update();
        };

        //MessageBus.Current.SendMessage();
        
        //MessageBus.Current.RegisterMessageSource(this);
    }
}