namespace LitePatch.Services.Interfaces;

public interface IGitInfoService
{
    public string RepositoryPath { get; set; }

    public bool ReadRepositoryInfo();
}