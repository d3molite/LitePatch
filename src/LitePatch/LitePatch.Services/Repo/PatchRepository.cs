using System.Management.Automation;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;

namespace LitePatch.Services.Repo;

public class PatchRepository(IGitInfoService gitInfoService, ISettingsService settingsService) : IPatchRepository
{
    
    public PatchInfo CreatePatchFile(string sha, string commitName, int counter)
    {
        var output = Path.Combine(settingsService.Settings.OutputFolderPath,
            $"{counter.ToString().PadLeft(4, '0')}-{CleanPatchName(commitName)}.patch");
        
        var command = @$"git format-patch -1 -B1% {sha} --output='{output}'";

        using var powershell = PowerShell.Create();
        powershell.AddScript(@$"cd {gitInfoService.RepositoryPath}");
        powershell.AddScript(command);

        var results = powershell.Invoke();
        
        return new PatchInfo(commitName, output);
        
    }
    
    private static string CleanPatchName(string input)
    {
        var ret = input.Trim();
        ret = ret.Replace(" ", "_");
        ret = ret.Replace(Environment.NewLine, "");
        ret = ret.Replace("\n", "");
        
        return ret;
    }

    private List<PatchInfo> BuildCleanPatchList(List<string> rawPatchList)
    {
        List<PatchInfo> patchList = new();

        foreach (var patchNamePath in rawPatchList)
        {
            var patchName = Path.GetFileNameWithoutExtension(patchNamePath);
            patchList.Add(new PatchInfo(patchName, patchNamePath));
        }

        return patchList;
    }

    public List<PatchInfo> LoadPatchesToList(string path)
    {
        var command = $"Get-ChildItem -Path {path} -Recurse -Include *.patch";
        using var powershell = PowerShell.Create();
        powershell.AddScript(command);
        var results = powershell.Invoke();

        var rawPatchList = results.Select(result => result.ToString()).ToList();        
        
        return BuildCleanPatchList(rawPatchList);
    }

    public bool ApplyPatchFile(PatchInfo patchInfo)
    {
        var command = $"git apply -3 --3 --ignore-space-change --ignore-whitespace {patchInfo.PatchPath}";

        using var powershell = PowerShell.Create();
        powershell.AddScript(@$"cd {gitInfoService.RepositoryPath}");
        powershell.AddScript(command);
        
        //Take a look at results and evaluate displaying potential errors if direct powershell messages
        
        if (powershell.Invoke() != null)
        {
            patchInfo.HasBeenApplied = true;
            return true;
        }
        
        return false;
    }
    
}