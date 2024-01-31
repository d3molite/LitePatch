﻿using System.Windows;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Repo;
using LitePatch.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;

namespace LitePatch;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        var collection = new ServiceCollection();
        collection.AddWpfBlazorWebView();
        collection.AddBlazorWebViewDeveloperTools();
        collection.AddMudServices();

        collection.AddSingleton<ISettingsRepository, SettingsRepository>();
        collection.AddSingleton<IPatchRepository, PatchRepository>();
        collection.AddSingleton<ISettingsService, SettingsService>();
        collection.AddSingleton<IGitInfoService, GitInfoService>();
        collection.AddSingleton<IGitPatchService, GitPatchService>();
  

        
        Resources.Add("services", collection.BuildServiceProvider());
    }
}