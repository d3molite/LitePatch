using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;


namespace LitePatch.Services.Services;

public class GitPatchService(IPatchRepository patchRepository) : IGitPatchService
{
    private int _counter = 1;

    public List<PatchInfo> PatchList { get; set; }  = new();
    public string PatchFolderPath { get; set; } = string.Empty;

    public void ExportPatch(string sha, string commitName)
    {
        patchRepository.CreatePatchFile(sha, commitName, _counter);
        _counter++;
    }
    
    public void ApplyPatch(PatchInfo patch)
    {
        patchRepository.ApplyPatchFile(patch);
    }

    public bool LoadPatchesFromFolder()
    {
        if (string.IsNullOrEmpty(PatchFolderPath)) return false;

        try
        {
            PatchList = patchRepository.LoadPatchesToList(PatchFolderPath);
        }
        catch
        {
            return false;
        }

        return true;
    }

}