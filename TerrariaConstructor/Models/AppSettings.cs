using System;
using Autofac;
using LiteDB;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TerrariaConstructor.Infrastructure;

namespace TerrariaConstructor.Models;

public class AppSettings : ReactiveObject
{
    #region Properties

    public ObjectId Id { get; set; }
    public bool ShowTooltips { get; set; }
    public bool IsConstructorMode { get; set; }
    [Reactive]
    public bool IsManagerMode { get; set; }
    public string PlayersPath { get; set; }

    #endregion

    public void LoadSettings()
    {
        using (var scope = App.Container.BeginLifetimeScope())
        {
            var unitOfWork = scope.Resolve<UnitOfWork>();
            var settings = unitOfWork.SettingsRepository.LoadSettings();

            if (settings != null)
            {
                IsConstructorMode = settings.IsConstructorMode;
                IsManagerMode = settings.IsManagerMode;
                ShowTooltips = settings.ShowTooltips;
                PlayersPath = settings.PlayersPath;
            }
        }
    }

    public void SaveSettings()
    {
        using (var scope = App.Container.BeginLifetimeScope())
        {
            var unitOfWork = scope.Resolve<UnitOfWork>();
            unitOfWork.SettingsRepository.SaveSettings(this);
        }
    }

}