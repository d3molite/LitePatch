namespace LitePatch.Services.Interfaces;

public interface IPatchInfo
{
    public string PatchName { get; }
    
    public string PatchPath { get; }
    
    public bool HasBeenApplied { get; set; }

}