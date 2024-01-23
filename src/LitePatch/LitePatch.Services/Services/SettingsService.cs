using System.Text.Json;
using System.Text.Json.Serialization;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;
using LitePatch.Services.Repo;

namespace LitePatch.Services.Services;

public class SettingsService : ISettingsService
{
    public SettingsRepository SettingsRepository { get; set; }
    public ApplicationSetting Settings { get; set; }

    public SettingsService()
    {
        SettingsRepository = new SettingsRepository();
        Settings = SettingsRepository.ReadSettings();
    }

    public void Save()
    {
        SettingsRepository.WriteSettings(Settings);
    }
    
}