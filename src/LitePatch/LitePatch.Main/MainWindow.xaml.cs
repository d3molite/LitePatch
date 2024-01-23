using System.Windows;
using LitePatch.Services.Interfaces;
using LitePatch.Services.Services;
using Microsoft.Extensions.DependencyInjection;
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

        collection.AddSingleton<IGitInfoService, GitInfoService>();
        collection.AddSingleton<IGitPatchService, GitPatchService>();
        
        Resources.Add("services", collection.BuildServiceProvider());
    }
}