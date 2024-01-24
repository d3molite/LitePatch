using System.Text.Json;
using System.Text.Json.Serialization;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;
using LitePatch.Services.Repo;

namespace LitePatch.Services.Services;

public class SettingsService : ISettingsService
{
    private readonly ISettingsRepository _settingsRepository;
    public ApplicationSetting Settings { get; set; }

    public SettingsService(ISettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
        Settings = _settingsRepository.ReadSettings();
    }

    public void Save()
    {
        _settingsRepository.WriteSettings(Settings);
    }
    
}