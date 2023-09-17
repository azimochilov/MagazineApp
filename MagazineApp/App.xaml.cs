using MagazineApp.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MagazineApp.Windows;
using MagazineApp.Data.IRepositories;
using MagazineApp.Data.Repositories;
using MagazineApp.Service.Mappers;
using MagazineApp.Service.Interfaces;
using MagazineApp.Service.Services;

namespace MagazineApp;
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;
    public static IHost AppHost { get; private set; }

    public App()
    {
        _serviceProvider = ConfigureServices();
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<LoginWindow>();
                services.AddScoped<RegisterWindow>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IStoreService, StoreService>();
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddAutoMapper(typeof(MapperProfile));
            })
            .Build();
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

    //protected override async void OnStartup(StartupEventArgs e)
    //{
    //    await AppHost!.StartAsync();
    //    var startupForm = AppHost.Services.GetRequiredService<LoginWindow>();
    //    startupForm.Show();
    //    base.OnStartup(e);
    //}
    //protected override async void OnExit(ExitEventArgs e)
    //{
    //    await AppHost!.StopAsync();
    //    base.OnExit(e);
    //}
}
