namespace LitePatch.Services.Interfaces;

public interface IGitPatchService
{
    public void CreatePatch(string sha, string commitName);

    public void ApplyPatch();
}