using LibGit2Sharp;
using LitePatch.Services.Interfaces;

namespace LitePatch.Services.Services;

public class GitInfoService : IGitInfoService
{
    public string RepositoryPath { get; set; } = string.Empty;
    
    public Repository CurrentRepository { get; set; }
    
    public Branch CurrentBranch { get; set; }
    
    public List<Commit> CommitsOnBranch { get; set; }  = new();

    public GitInfoService()
    {
        
    }
    
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