using LibGit2Sharp;

namespace LitePatch.Services.Interfaces;

public interface IGitInfoService
{
    public Repository CurrentRepository { get; set; }
    
    public Branch CurrentBranch { get; set; }

    public List<Commit> CommitsOnBranch { get; set; }
    
    public string RepositoryPath { get; }

    public bool ReadRepositoryInfo();

    public event EventHandler InfoChanged;
    
}