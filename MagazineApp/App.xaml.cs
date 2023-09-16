using MagazineApp.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System;
using Microsoft.EntityFrameworkCore;

namespace MagazineApp;
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        _serviceProvider = ConfigureServices();
    }

    private IServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        // Register your DbContext
        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=Yaironman@.26;Port=3306;";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        // Add other services or dependencies as needed

        // Build the service provider
        return serviceCollection.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Continue with your application startup logic
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

}
