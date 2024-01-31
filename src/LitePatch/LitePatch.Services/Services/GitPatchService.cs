using System.Management.Automation;
using LibGit2Sharp;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Models;
using LitePatch.Services.Repo;

namespace LitePatch.Services.Services;

public class GitPatchService() : IGitPatchService
{

    private readonly IPatchRepository _patchRepository;
    private readonly ISettingsService _settingsService;

    public GitPatchService(IPatchRepository patchRepository, ISettingsService settingsService) : this()
    {
        _patchRepository = patchRepository;
        var outputFolderPath = settingsService.Settings.OutputFolderPath;

        if (!string.IsNullOrEmpty(outputFolderPath))
        {
            PatchList = patchRepository.LoadPatchesToList(outputFolderPath);
        }
    }
    
    public List<PatchInfo> PatchList { get; set; }  = new();
    public string RepoFolderPath { get; set; } = string.Empty;

    public void ExportPatch(string sha, string commitName)
    {
        PatchList.Add(_patchRepository.CreatePatchFile(sha, commitName));
    }
    

    public void ApplyPatch(PatchInfo patch)
    {
        _patchRepository.ApplyPatchFile(patch);
    }

    


}