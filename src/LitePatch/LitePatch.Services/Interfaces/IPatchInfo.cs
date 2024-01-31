using LibGit2Sharp;
using LitePatch.Services.Models;

namespace LitePatch.Services.Interfaces;

public interface IPatchInfo
{
    string GetPatchFilePath();
    void MarkAsApplied();

}