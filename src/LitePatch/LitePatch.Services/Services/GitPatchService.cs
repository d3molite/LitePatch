using System.Management.Automation;
using LitePatch.Services.Interfaces;

namespace LitePatch.Services.Services;

public class GitPatchService(IGitInfoService gitInfoService, ISettingsService settingsService) : IGitPatchService
{
    private int _counter = 1;
    
    public void CreatePatch(string sha, string commitName)
    {
        var output = Path.Combine(settingsService.Settings.OutputFolderPath,
            $"{_counter.ToString().PadLeft(4, '0')}-{CleanPatchName(commitName)}.patch");
        
        var command = @$"git format-patch -1 -B1% {sha} --output='{output}'";

        using var powershell = PowerShell.Create();
        powershell.AddScript(@$"cd {gitInfoService.RepositoryPath}");
        powershell.AddScript(command);

        var results = powershell.Invoke();
        _counter++;
    }

    public void ApplyPatch()
    {
        throw new NotImplementedException();
    }

    private string CleanPatchName(string input)
    {
        var ret = input.Trim();
        ret = ret.Replace(" ", "_");
        ret = ret.Replace(Environment.NewLine, "");
        ret = ret.Replace("\n", "");
        return ret;
    }
}