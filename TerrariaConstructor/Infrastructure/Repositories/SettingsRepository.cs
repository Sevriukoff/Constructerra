using System.Linq;
using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Repositories;

public class SettingsRepository : ISettingsRepository
{
    private readonly ILiteDatabase _playersDatabase;
    private const string SettingsTableName = "settings";

    public SettingsRepository(ILiteDatabase playersDatabase)
    {
        _playersDatabase = playersDatabase;
    }
    
    public bool SaveSettings(AppSettings settings)
    {
        var settingsCollection = _playersDatabase.GetCollection<AppSettings>(SettingsTableName);

        // Check if any settings exist in the collection
        var existingSettings = settingsCollection.FindAll().FirstOrDefault();

        if (existingSettings != null)
        {
            // Update the existing settings document
            existingSettings.ShowTooltips = settings.ShowTooltips;
            existingSettings.IsConstructorMode = settings.IsConstructorMode;
            existingSettings.IsManagerMode = settings.IsManagerMode;
            existingSettings.PlayersPath = settings.PlayersPath;

            return settingsCollection.Update(existingSettings);
        }
        else
        {
            // Insert the new settings document
            return settingsCollection.Insert(settings);
        }
    }

    public AppSettings LoadSettings()
    {
        var settingsCollection = _playersDatabase.GetCollection<AppSettings>(SettingsTableName);
        return settingsCollection.FindOne(Query.All());
    }
}