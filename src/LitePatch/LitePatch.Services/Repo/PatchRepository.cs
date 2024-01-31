using System.Management.Automation;
using LibGit2Sharp;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;

namespace LitePatch.Services.Repo;

public class PatchRepository(IGitInfoService gitInfoService, ISettingsService settingsService) : IPatchRepository
{
    private int _counter = 1;
    
    public PatchInfo CreatePatchFile(string sha, string commitName)
    {
        var output = Path.Combine(settingsService.Settings.OutputFolderPath,
            $"{_counter.ToString().PadLeft(4, '0')}-{CleanPatchName(commitName)}.patch");
        
        var command = @$"git format-patch -1 -B1% {sha} --output='{output}'";

        using var powershell = PowerShell.Create();
        powershell.AddScript(@$"cd {gitInfoService.RepositoryPath}");
        powershell.AddScript(command);

        var results = powershell.Invoke();
        _counter++;
        
        return new PatchInfo(commitName, output);
        
    }
    
    private string CleanPatchName(string input)
    {
        var ret = input.Trim();
        ret = ret.Replace(" ", "_");
        ret = ret.Replace(Environment.NewLine, "");
        ret = ret.Replace("\n", "");
        return ret;
    }

    private List<PatchInfo> BuildCleanPatchList(List<string> rawPatchList, string path)
    {
        List<PatchInfo> patchList = new();

        foreach (var patchNamePath in rawPatchList)
        {
            var patchPath = patchNamePath;
            var patchName = System.IO.Path.GetFileNameWithoutExtension(patchNamePath);
            patchList.Add(new PatchInfo(patchName, patchPath));
        }

        return patchList;
    }

    public List<PatchInfo> LoadPatchesToList(string path)
    {
        var command = $"Get-ChildItem -Path {path} -Recurse -Include *.patch";
        using var powershell = PowerShell.Create();
        powershell.AddScript(command);
        var results = powershell.Invoke();

        List<string> rawPatchList = results.Select(result => result.ToString()).ToList();        
        
        return BuildCleanPatchList(rawPatchList, path);
    }

    public bool ApplyPatchFile(PatchInfo patch)
    {
        var command = $"git apply -3 --3 --ignore-space-change --ignore-whitespace {patch.GetPatchFilePath()}";

        using var powershell = PowerShell.Create();
        powershell.AddScript(@$"cd {gitInfoService.RepositoryPath}");
        powershell.AddScript(command);

        if (powershell.Invoke() != null)
        {
            patch.MarkAsApplied();
            return true;
        }

        return false;

    }
}