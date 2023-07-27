using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface ISettingsRepository
{
    bool SaveSettings(AppSettings settings);
    AppSettings LoadSettings();
}