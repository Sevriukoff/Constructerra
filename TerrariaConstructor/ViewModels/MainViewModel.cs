using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Win32;
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
    private readonly ISnackbarService _snackbarService;
    private ObservableCollection<object> _navigationItems;
    private ObservableCollection<object> _footerItems;
    public PlayerModel PlayerModel { get; }
    public CharacteristicsViewModel CharacteristicsViewModel { get; set; }
    public EquipsViewModel EquipsViewModel { get; set; }

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

    public InventoriesViewModel InventoriesViewModel { get; set; }

    public string Title => "ConstrucTerra - создавай своих героев";

    public MainViewModel(PlayerModel playerModel,
        CharacteristicsViewModel characteristicsViewModel,
        EquipsViewModel equipsViewModel,
        InventoriesViewModel inventoriesViewModel,
        ISnackbarService snackbarService)
    {
        PlayerModel = playerModel;
        CharacteristicsViewModel = characteristicsViewModel;
        EquipsViewModel = equipsViewModel;
        InventoriesViewModel = inventoriesViewModel;
        _snackbarService = snackbarService;

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
                TargetPageType = typeof(InventoriesView),
                MenuItems = new ObservableCollection<object>
                {
                    new NavigationViewItem
                    {
                        Content = "Основной инветарь",
                        TargetPageType = typeof(MainInventoryView)
                    },
                    new NavigationViewItem
                    {
                        Content = "Копилка",
                        TargetPageType = typeof(CharacteristicsView)
                    },
                    new NavigationViewItem
                    {
                        Content = "Сейф",
                        TargetPageType = typeof(CharacteristicsView)
                    },
                    new NavigationViewItem
                    {
                        Content = "Кузница",
                        TargetPageType = typeof(CharacteristicsView)
                    },
                    new NavigationViewItem
                    {
                        Content = "Безднонный мешок",
                        TargetPageType = typeof(CharacteristicsView)
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
            
        };
        
        FooterItems.Add(uploadNavigationViewItem);
        FooterItems.Add(saveNavigationViewItem);
        FooterItems.Add(createNavigationViewItem);
        
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