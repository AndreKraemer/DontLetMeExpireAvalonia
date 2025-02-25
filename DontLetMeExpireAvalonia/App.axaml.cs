using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using DontLetMeExpireAvalonia.ViewModels;
using DontLetMeExpireAvalonia.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DontLetMeExpireAvalonia.Services;

namespace DontLetMeExpireAvalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
       
        // Configure IoC
        var services = new ServiceCollection();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ShellViewModel>();
        services.AddSingleton<IStorageLocationService, DummyStorageLocationService>();
        services.AddSingleton<IItemService, DummyItemService>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<ItemViewModel>();
        services.AddTransient<ItemsViewModel>();
        var provider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = provider.GetRequiredService<ShellViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new ShellView
            {
                DataContext = provider.GetRequiredService<ShellViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}