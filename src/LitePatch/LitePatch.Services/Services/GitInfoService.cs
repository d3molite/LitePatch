using LibGit2Sharp;
using LitePatch.Services.Interfaces;

namespace LitePatch.Services.Services;

public class GitInfoService : IGitInfoService
{
    public string RepositoryPath { get; set; }
    
    public Branch CurrentBranch { get; set; }
    
    public List<Commit> CommitsOnBranch { get; set; }

    public GitInfoService()
    {
        
    }
    
    public bool ReadRepositoryInfo()
    {
        if (string.IsNullOrEmpty(RepositoryPath)) return false;

        try
        {
            var repo = new Repository(RepositoryPath);
            CurrentBranch = repo.Head;
            CommitsOnBranch = CurrentBranch.Commits.Take(10).ToList();
        }
        catch
        {
            return false;
        }

        return true;
    }
}