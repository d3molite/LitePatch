using System.Text.Json;
using System.Text.Json.Serialization;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;

namespace LitePatch.Services.Services;

public class SettingsService : ISettingsService
{
    public ApplicationSetting Settings { get; set; }

    public SettingsService()
    {
        Settings = ReadSettings();
    }

    public void Save()
    {
        WriteSettings();
    }

    private readonly string _settingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
        "LitePatch");

    private const string SettingsFile = "settings.json";

    private string SettingsFilePath => Path.Combine(_settingsPath, SettingsFile);

    private void CheckForPath()
    {
        if (!Directory.Exists(_settingsPath))
            Directory.CreateDirectory(_settingsPath);
    }
    
    private ApplicationSetting ReadSettings()
    {
        CheckForPath();

        if (!File.Exists(SettingsFilePath))
        {
            return new ApplicationSetting();
        }

        var text = File.ReadAllText(SettingsFilePath);
        return JsonSerializer.Deserialize<ApplicationSetting>(text)!;
    }

    private void WriteSettings()
    {
        CheckForPath();

        var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        
        File.WriteAllText(SettingsFilePath, json);
    }
}