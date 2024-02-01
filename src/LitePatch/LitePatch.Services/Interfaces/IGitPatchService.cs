using LitePatch.Services.Models;

namespace LitePatch.Services.Interfaces;

public interface IGitPatchService
{
    public List<PatchInfo> PatchList { get; set; }

    public string PatchFolderPath { get; set; }

    public bool ExportPatch(string sha, string commitName);

    public bool ApplyPatch(PatchInfo patch);

    public bool LoadPatchesFromFolder();
}