using System.Management.Automation;
using LitePatch.Services.Interfaces;

namespace LitePatch.Services.Services;

public class GitPatchService(IGitInfoService gitInfoService) : IGitPatchService
{
    public void CreatePatch(string sha, string outputDir)
    {
        var command = @$"git format-patch -1 -B1% {sha} -o '{outputDir}'";

        using var powershell = PowerShell.Create();
        powershell.AddScript(@$"cd {gitInfoService.RepositoryPath}");
        powershell.AddScript(command);

        var results = powershell.Invoke();
    }
}