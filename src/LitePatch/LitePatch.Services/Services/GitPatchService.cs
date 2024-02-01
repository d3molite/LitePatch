using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;


namespace LitePatch.Services.Services;

public class GitPatchService(IPatchRepository patchRepository) : IGitPatchService
{
    private int _counter = 1;

    public List<PatchInfo> PatchList { get; set; } = new();
    public string PatchFolderPath { get; set; } = string.Empty;

    public bool ExportPatch(string sha, string commitName)
    {
        if (patchRepository.CreatePatchFile(sha, commitName, _counter))
        {
            _counter++;
            return true;
        }

        return false;
    }

    public bool ApplyPatch(PatchInfo patch)
    {
        if (patchRepository.ApplyPatchFile(patch))
        {
            return true;
        }

        return false;
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