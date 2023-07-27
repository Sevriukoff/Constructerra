using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TerrariaConstructor.Models;
using Wpf.Ui.Common;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls;

namespace TerrariaConstructor.ViewModels;

public class PlayersViewModel : ReactiveObject
{
    private readonly AppSettings _appSettings;
    private readonly ISnackbarService _snackbarService;
    private readonly IDialogService _dialogService;
    private readonly PlayerModel _playerModel;

    public ObservableCollection<PlayerDto> Players { get; set; }

    public ReactiveCommand<PlayerDto, Unit> UploadPlayerCommand { get; }
    public ReactiveCommand<PlayerDto, Unit> DeletePlayerCommand { get; }

    public PlayersViewModel(AppSettings appSettings, ISnackbarService snackbarService, IDialogService dialogService,
        PlayerModel playerModel)
    {
        _appSettings = appSettings;
        _snackbarService = snackbarService;
        _dialogService = dialogService;
        _playerModel = playerModel;

        _appSettings.LoadSettings();

        Players = new ObservableCollection<PlayerDto>(
            Directory.GetFiles(_appSettings.PlayersPath)
                .Where(x => x[^3..] == "plr")
                .Select(x =>
                {
                    var player = _playerModel.GetPlayer(x);
                    player.IsSelected = player.FilePath == _playerModel.FilePath;
                    return player;
                }));

        UploadPlayerCommand = ReactiveCommand.Create<PlayerDto>(x =>
        {
            try
            {
                _playerModel.LoadPlayer(x.FilePath);

                foreach (var player in Players)
                {
                    player.IsSelected = player.FilePath == x.FilePath;
                }

                _snackbarService.Show("Успешно", $"Персонаж {x.Name} был загружен", SymbolRegular.ArrowUpload16,
                    ControlAppearance.Success);
            }
            catch (Exception e)
            {
                _snackbarService.Show("Ошибка",
                    $"При загрузке {x.Name} произошла ошибка. Возможно файл повреждён или имеет не правильный формат",
                    SymbolRegular.ArrowUpload16,
                    ControlAppearance.Danger);
            }
        });

        DeletePlayerCommand = ReactiveCommand.Create<PlayerDto>(x =>
        {
            var rootDialog = _dialogService.GetDialogControl();

            rootDialog.DialogHeight = 240;
            rootDialog.ButtonRightClick += (_, _) => rootDialog.Hide();
            rootDialog.ButtonLeftClick += (_, _) =>
            {
                try
                {
                    if (File.Exists(x.FilePath))
                        File.Delete(x.FilePath);
                    
                    Players.Remove(x);
                    _playerModel.CreateNewPlayer();

                    rootDialog.Hide();
                    _snackbarService.Show("Успешно", $"Персонаж {x.Name} был удалён", SymbolRegular.ArrowUpload16,
                        ControlAppearance.Success);
                }
                catch (Exception e)
                {
                    _snackbarService.Show("Ошибка",
                        $"При удалении {x.Name} произошла ошибка. Возможно файл занят другим процессом или более не доступен",
                        SymbolRegular.ArrowUpload16,
                        ControlAppearance.Danger);
                }
            };

            rootDialog.Show();
        });
    }
}

public class PlayerDto : ReactiveObject
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; set; }
    public int Version { get; set; }
    public uint Revision { get; set; }
    public byte Difficulty { get; set; }
    public bool IsFavorite { get; set; }
    public TimeSpan PlayTime { get; set; }
    public string FilePath { get; set; }
    [Reactive]
    public bool IsSelected { get; set; }

    public Item[] Equips { get; set; }
}