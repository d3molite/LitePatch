using LitePatch.Services.Interfaces;

namespace LitePatch.Services.Models;

public class PatchInfo(string commitName, string patchPath) : IPatchInfo
{
    public string PatchName { get; } = commitName;
    
    public string PatchPath { get; } = patchPath;
    
    public bool HasBeenApplied { get; set; } = false; 
    
}