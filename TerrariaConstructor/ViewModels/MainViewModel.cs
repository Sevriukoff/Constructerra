using System;
using System.Collections.ObjectModel;
using System.IO;
using Autofac;
using Microsoft.Win32;
using ReactiveUI;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;
using TerrariaConstructor.Services;
using TerrariaConstructor.Views;
using Wpf.Ui.Common;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.ViewModels;

public class MainViewModel : ReactiveObject
{
    private readonly ISnackbarService _snackbarService;
    private ObservableCollection<object> _navigationItems;
    private ObservableCollection<object> _footerItems;
    public PlayerModel PlayerModel { get; }

    public ObservableCollection<object> NavigationItems
    {
        get => _navigationItems;
        set => this.RaiseAndSetIfChanged(ref _navigationItems, value);
    }

    public ObservableCollection<object> FooterItems
    {
        get => _footerItems;
        set => this.RaiseAndSetIfChanged(ref _footerItems, value);
    }
    
    public string Title => "ConstrucTerra - создавай своих героев";

    public MainViewModel(PlayerModel playerModel, ISnackbarService snackbarService)
    {
        PlayerModel = playerModel;
        _snackbarService = snackbarService;

        NavigationItems = new ObservableCollection<object>
        {
            new NavigationViewItem
            {
                Content = "Приветствие",
                Icon = new SymbolIcon{Symbol = SymbolRegular.Star12},
                TargetPageType = typeof(WelcomeView)
            },
            new NavigationViewItem
            {
                Content = "Характеристика",
                ToolTip = "Основные характеристики персонажа",
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
                TargetPageType = typeof(InventoriesView),
                MenuItems = new ObservableCollection<object>
                {
                    new NavigationViewItem
                    {
                        Content = "Основной инветарь",
                        TargetPageType = typeof(MainInventoryView),
                        DataContext = App.Container.Resolve<MainInventoryViewModel>()
                    },
                    new NavigationViewItem
                    {
                        Content = "Копилка",
                        TargetPageType = typeof(MainInventoryView),
                        DataContext = App.Container.Resolve<PiggyInventoryViewModel>()
                    },
                    new NavigationViewItem
                    {
                        Content = "Сейф",
                        TargetPageType = typeof(MainInventoryView),
                        DataContext = App.Container.Resolve<SafeInventoryViewModel>()
                    },
                    new NavigationViewItem
                    {
                        Content = "Кузница",
                        TargetPageType = typeof(MainInventoryView),
                        DataContext = App.Container.Resolve<ForgeInventoryViewModel>()
                    },
                    new NavigationViewItem
                    {
                        Content = "Безднонный мешок",
                        TargetPageType = typeof(MainInventoryView),
                        DataContext = App.Container.Resolve<VoidInventoryViewModel>()
                    }
                }
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

        FooterItems = new ObservableCollection<object>();

        var uploadNavigationViewItem = new NavigationViewItem
        {
            Content = "Загрузить",
            Icon = new SymbolIcon {Symbol = SymbolRegular.ArrowUpload16},
        };
        PlayerModel.CreateNewPlayer();

        uploadNavigationViewItem.Click += (_, _) =>
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string playerName = Path.GetFileNameWithoutExtension(filePath);
                _snackbarService.Timeout = 3000;
                
                try
                {
                    PlayerModel.LoadPlayer(filePath);
                }
                catch (Exception e)
                {
                    _snackbarService.Show("Ошибка",
                        $"При загрузке {playerName} произошла ошибка. Возможно файл повреждён или имеет не правильный формат",
                        SymbolRegular.ArrowUpload16,
                        ControlAppearance.Danger);
                    
                    return;
                }
                
                _snackbarService.Show("Успешно", $"Персонаж {playerName} был загружен", SymbolRegular.ArrowUpload16,
                    ControlAppearance.Success);
            }
        };
        
        var saveNavigationViewItem = new NavigationViewItem
        {
            Content = "Сохранить",
            Icon = new SymbolIcon {Symbol = SymbolRegular.ArrowDownload16},
        };

        saveNavigationViewItem.Click += (_, _) =>
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Plr files (*.plr)|*.plr";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = "Name";

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                PlayerModel.SavePlayer(fileName);
            }
        };
        
        var createNavigationViewItem = new NavigationViewItem
        {
            Content = "Создать",
            Icon = new SymbolIcon {Symbol = SymbolRegular.Add12},
        };

        createNavigationViewItem.Click += (_, _) =>
        {
            PlayerModel.CreateNewPlayer();
            
            _snackbarService.Timeout = 3000;
            _snackbarService.Show("Оповещение", $"Новый персонаж был создан, можете приступить к его настройке", SymbolRegular.Add16,
                ControlAppearance.Info);
        };
        
        FooterItems.Add(uploadNavigationViewItem);
        FooterItems.Add(saveNavigationViewItem);
        FooterItems.Add(createNavigationViewItem);
        
        
        PlayerModel.PlayerUpdated += () =>
        {
            MessageBus.Current.SendMessage(new PlayerUpdatedEvent());
        };

        //MessageBus.Current.SendMessage();
        
        //MessageBus.Current.RegisterMessageSource(this);
    }
}