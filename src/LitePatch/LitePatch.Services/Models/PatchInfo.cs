using LitePatch.Services.Interfaces;

namespace LitePatch.Services.Models;

public class PatchInfo : IPatchInfo
{
    private string _patchName { get; }
    private string _patchPath { get; }
    private bool _hasbeenApplied;

    public PatchInfo(string commitName, string patchPath)
    {
        _patchName = commitName;
        _patchPath = patchPath;
        _hasbeenApplied = false;

    }

    public string GetPatchName()
    {
        return _patchName;
    }
    
    public string GetPatchFilePath()
    {
        return _patchPath;
    }

    public bool AppliedStatus()
    {
        return _hasbeenApplied;
    }

    public void MarkAsApplied()
    {
        _hasbeenApplied = true;
    }
}