using LitePatch.Services.Models;

namespace LitePatch.Services.Interfaces;

public interface ISettingsRepository
{
    public ApplicationSetting ReadSettings();

    public void WriteSettings(ApplicationSetting settings);

}