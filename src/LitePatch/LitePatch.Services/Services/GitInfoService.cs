using LibGit2Sharp;
using LitePatch.Services.Interfaces;

namespace LitePatch.Services.Services;

public class GitInfoService : IGitInfoService
{
    private readonly ISettingsService _settingsService;

    public GitInfoService(ISettingsService settingsService)
    {
        _settingsService = settingsService;

        if (!string.IsNullOrEmpty(settingsService.Settings.RepositoryPath))
        {
            ReadRepositoryInfo();
        }
    }

    public string RepositoryPath => _settingsService.Settings.RepositoryPath;
    
    public Repository CurrentRepository { get; set; }
    
    public Branch CurrentBranch { get; set; }
    
    public List<Commit> CommitsOnBranch { get; set; }  = new();

    public bool ReadRepositoryInfo()
    {
        if (string.IsNullOrEmpty(RepositoryPath)) return false;

        try
        {
            CurrentRepository = new Repository(RepositoryPath);
            CurrentBranch = CurrentRepository.Head;
            CommitsOnBranch = CurrentBranch.Commits.Take(10).ToList();
            InfoChanged?.Invoke(this, EventArgs.Empty);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public event EventHandler? InfoChanged;
}