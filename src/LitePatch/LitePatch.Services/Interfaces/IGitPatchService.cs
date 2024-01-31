using LitePatch.Services.Models;

namespace LitePatch.Services.Interfaces;

public interface IGitPatchService
{
    public List<PatchInfo> PatchList { get; set; }
    public string RepoFolderPath { get; set; }
    public void ExportPatch(string sha, string commitName);
    public void ApplyPatch(PatchInfo patch);

    


}