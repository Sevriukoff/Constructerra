using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using Autofac;
using Microsoft.Win32;
using ReactiveUI;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;
using TerrariaConstructor.Services;
using TerrariaConstructor.ViewModels.Inventories;
using TerrariaConstructor.Views;
using Wpf.Ui.Common;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.ViewModels;

public class MainViewModel : ReactiveObject
{
    private readonly ISnackbarService _snackbarService;
    private readonly AppSettings _appSettings;
    private ObservableCollection<object> _navigationItems;
    private ObservableCollection<object> _footerItems;
    private readonly PlayerModel _playerModel;

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

    public MainViewModel(PlayerModel playerModel, ISnackbarService snackbarService, AppSettings appSettings)
    {
        _playerModel = playerModel;
        _snackbarService = snackbarService;
        _appSettings = appSettings;
        _appSettings.LoadSettings();

        var navigationPlayersItem = new NavigationViewItem
        {
            Content = "Персонажи",
            Icon = new SymbolIcon { Symbol = SymbolRegular.PeopleCommunity20 },
            TargetPageType = typeof(PlayersView)
        };

        var binding = new Binding("IsManagerMode")
        {
            Source = _appSettings,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            Mode = BindingMode.TwoWay // Установите режим привязки в OneWay, если вы хотите только просматривать значения свойства
        };

        BindingOperations.SetBinding(navigationPlayersItem, NavigationViewItem.IsEnabledProperty, binding);
        
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
            },
            navigationPlayersItem,
            new NavigationViewItem
            {
                Content = "Настройки",
                Icon = new SymbolIcon{Symbol = SymbolRegular.Settings16},
                TargetPageType = typeof(SettingsView)
            },
        };

        FooterItems = new ObservableCollection<object>();

        var uploadNavigationViewItem = new NavigationViewItem
        {
            Content = "Загрузить",
            Icon = new SymbolIcon {Symbol = SymbolRegular.ArrowUpload16}
        };
        _playerModel.CreateNewPlayer();

        uploadNavigationViewItem.Click += (_, _) =>
        {
            var openFileDialog = new OpenFileDialog { Filter = "Plr files (*.plr)|*.plr" };
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string playerName = Path.GetFileNameWithoutExtension(filePath);
                
                try
                {
                    _playerModel.LoadPlayer(filePath);
                    _snackbarService.Show("Успешно", $"Персонаж {playerName} был загружен", SymbolRegular.ArrowUpload16,
                        ControlAppearance.Success);
                }
                catch (Exception e)
                {
                    _snackbarService.Show("Ошибка",
                        $"При загрузке {playerName} произошла ошибка. Возможно файл повреждён или имеет не правильный формат",
                        SymbolRegular.ArrowUpload16,
                        ControlAppearance.Danger);
                }
            }
        };
        
        var saveNavigationViewItem = new NavigationViewItem
        {
            Content = "Сохранить",
            Icon = new SymbolIcon {Symbol = SymbolRegular.ArrowDownload16}
        };

        saveNavigationViewItem.Click += (_, _) =>
        {
            if (_appSettings.IsManagerMode)
            {
                try
                {
                    if (File.Exists(_playerModel.FilePath))
                        File.Delete(_playerModel.FilePath);

                    _playerModel.SavePlayer(_playerModel.FilePath);
                    _snackbarService.Show("Успешно", $"Персонаж {_playerModel._characteristic.Name} был сохранён. Можете погамать",
                        SymbolRegular.ArrowUpload16,
                        ControlAppearance.Success);
                }
                catch (Exception e)
                {
                    _snackbarService.Show("Ошибка",
                        $"При сохранении {_playerModel._characteristic.Name} произошла ошибка.",
                        SymbolRegular.ArrowUpload16,
                        ControlAppearance.Danger);
                }
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Plr files (*.plr)|*.plr";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.FileName = "Name";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string fileName = saveFileDialog.FileName;
                    try
                    {
                        _playerModel.SavePlayer(fileName);
                        _snackbarService.Show("Успешно", $"Персонаж {_playerModel._characteristic.Name} был сохранён по пути {fileName}",
                            SymbolRegular.ArrowUpload16,
                            ControlAppearance.Success);
                    }
                    catch (Exception e)
                    {
                        _snackbarService.Show("Ошибка",
                            $"При сохранении {_playerModel._characteristic.Name} произошла ошибка." +
                            $" Возможно у приложения нет доступа к директории: {Path.GetDirectoryName(fileName)}",
                            SymbolRegular.ArrowUpload16,
                            ControlAppearance.Danger);
                    }
                }
            }
        };
        
        var createNavigationViewItem = new NavigationViewItem
        {
            Content = "Создать",
            Icon = new SymbolIcon {Symbol = SymbolRegular.Add12}
        };

        createNavigationViewItem.Click += (_, _) =>
        {
            _playerModel.CreateNewPlayer();
            
            _snackbarService.Timeout = 3000;
            _snackbarService.Show("Оповещение", $"Новый персонаж был создан, можете приступить к его настройке", SymbolRegular.Add16,
                ControlAppearance.Info);
        };
        
        FooterItems.Add(uploadNavigationViewItem);
        FooterItems.Add(saveNavigationViewItem);
        FooterItems.Add(createNavigationViewItem);

        _playerModel.PlayerUpdated += () =>
        {
            MessageBus.Current.SendMessage(new PlayerUpdatedEvent());
        };

        //MessageBus.Current.SendMessage();
        
        //MessageBus.Current.RegisterMessageSource(this);
    }
}