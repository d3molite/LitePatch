using LitePatch.Services.Models;

namespace LitePatch.Services.Interfaces;

public interface IPatchRepository
{
    public PatchInfo CreatePatchFile(string sha, string commitName, int counter);
    
    public List<PatchInfo> LoadPatchesToList(string path);
    
    public bool ApplyPatchFile(PatchInfo patchInfo);

}