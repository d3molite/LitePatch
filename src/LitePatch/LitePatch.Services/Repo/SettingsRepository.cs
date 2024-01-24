using System.Text.Json;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;

namespace LitePatch.Services.Repo;

public class SettingsRepository : ISettingsRepository
{

    private readonly string _settingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
        "LitePatch");

    private const string SettingsFile = "settings.json";

    private string SettingsFilePath => Path.Combine(_settingsPath, SettingsFile);
    
    public ApplicationSetting ReadSettings()
    {
            CheckForPath();

            if (!File.Exists(SettingsFilePath))
            {
                return new ApplicationSetting();
            }
            
            var text = File.ReadAllText(SettingsFilePath);
            return JsonSerializer.Deserialize<ApplicationSetting>(text)!;
    }
    

    public void WriteSettings(ApplicationSetting settings)
    {
        CheckForPath();

        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        
        File.WriteAllText(SettingsFilePath, json);
    }
    
    private void CheckForPath()
    {
        if (!Directory.Exists(_settingsPath))
            Directory.CreateDirectory(_settingsPath);
    }
    
    

}