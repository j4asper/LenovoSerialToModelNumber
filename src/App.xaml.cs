using System.Net.Http;
using System.Windows;
using LenovoSerialToModelNumber.Options;
using LenovoSerialToModelNumber.Services;
using LenovoSerialToModelNumber.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LenovoSerialToModelNumber;

public partial class App : Application
{
    private readonly IHost host;
    
    public App()
    {
        var builder = Host.CreateDefaultBuilder();
        
        builder.UseDefaultServiceProvider(options =>
        {
            options.ValidateScopes = true;
            options.ValidateScopes = true;
        });
        
        builder.ConfigureServices(services =>
        {
            services.AddOptionsWithValidateOnStart<LenovoApiOptions>()
                .BindConfiguration(LenovoApiOptions.LenovoApi);
           
            services.AddOptionsWithValidateOnStart<CsvOutputOptions>()
                .BindConfiguration(CsvOutputOptions.CsvOutput);
            
            services.AddLogging();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<ILenovoApiService, LenovoApiService>();
            services.AddSingleton<ICsvService, CsvService>();
            services.AddSingleton<MainWindow>();
        });
        
        host = builder.Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await host.StartAsync();
        
        var startupForm = host.Services.GetRequiredService<MainWindow>();
        startupForm.Show();
        
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await host.StopAsync();
        
        base.OnExit(e);
    }
}