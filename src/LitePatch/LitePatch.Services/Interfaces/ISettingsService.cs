using LitePatch.Services.Models;

namespace LitePatch.Services.Interfaces;

public interface ISettingsService
{
    public ApplicationSetting Settings { get; set; }

    public void Save();
}