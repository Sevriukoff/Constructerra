using System;
using System.IO;
using System.Reactive;
using WinForms = System.Windows.Forms;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class SettingsViewModel : ReactiveObject
{
    #region Fields

    private readonly AppSettings _appSettings;
    private bool _showTooltips;
    private bool _isConstructorMode;
    private bool _isManagerMode;
    private string _playersFilePath;

    #endregion
    
    #region Properties

    public bool ShowTooltips
    {
        get => _showTooltips;
        set
        {
            if (_showTooltips != value)
            {
                _showTooltips = value;
                _appSettings.ShowTooltips = _showTooltips;
                _appSettings.SaveSettings();
                
                this.RaisePropertyChanged();
            }
        }
    }
    public bool IsConstructorMode
    {
        get => _isConstructorMode;
        set
        {
            if (_isConstructorMode != value)
            {
                _isConstructorMode = value;
                _appSettings.IsConstructorMode = _isConstructorMode;
                _appSettings.SaveSettings();
                
                this.RaisePropertyChanged();
            }
        }
    }
    public bool IsManagerMode
    {
        get => _isManagerMode;
        set
        {
            if (_isManagerMode != value)
            {
                _isManagerMode = value;
                _appSettings.IsManagerMode = _isManagerMode;
                _appSettings.SaveSettings();
                
                this.RaisePropertyChanged();
            }
        }
    }
    public string PlayersFilePath
    {
        get => _playersFilePath;
        set
        {
            if (_playersFilePath != value)
            {
                /// Ммм... смакота
                _playersFilePath = string.IsNullOrEmpty(value) ? Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "My Games", "Terraria", "Players") : value;
                
                _appSettings.PlayersPath = _playersFilePath;
                _appSettings.SaveSettings();
                
                this.RaisePropertyChanged();
            }
        }
    }

    #endregion

    public ReactiveCommand<Unit, Unit> BrowsePathCommand { get; }

    public SettingsViewModel(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _appSettings.LoadSettings();

        _showTooltips = _appSettings.ShowTooltips;
        _isConstructorMode = _appSettings.IsConstructorMode;
        _isManagerMode = _appSettings.IsManagerMode;
        _playersFilePath = _appSettings.PlayersPath;

        BrowsePathCommand = ReactiveCommand.Create(ShowCategorySelection);
    }
    
    private void ShowCategorySelection()
    {
        var dialog = new WinForms.FolderBrowserDialog();

        if (dialog.ShowDialog() == WinForms.DialogResult.OK)
        {
            PlayersFilePath = dialog.SelectedPath;
        }
    }
}