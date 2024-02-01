using LitePatch.Services.Models;
using LitePatch.Services.Repo;

namespace LitePatch.Services.Interfaces;

public interface ISettingsService
{
    
    public ApplicationSetting Settings { get; set; }
    
    public void Save();
    
}