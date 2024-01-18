using System.Management.Automation;
using LitePatch.Services.Interfaces;

namespace LitePatch.Services.Services;

public class GitPatchService : IGitPatchService
{
    public void CreatePatch(string sha)
    {
        var command = $"git format-patch -1 -B1% {sha} -o C:/";

        using var powershell = PowerShell.Create();
        powershell.AddScript($"cd {directory}");
        powershell.AddScript(command);

        var results = powershell.Invoke();
    }
}