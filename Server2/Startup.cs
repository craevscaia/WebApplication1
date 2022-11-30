using Server.Repositories;
using Server.Services.DataService;
using Server.Services.DistributionService;
using Server.Services.HealthCheckService;
using Server.Services.HealthService;
using Server.Services.HttpService;
using Server.Services.Sync;
using Server.Services.TcpService;
using Server.Setting;
using HealthCheckService = Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckService;
using HealthService = Server.Services.HealthService.HealthService;

namespace Server;

public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddLogging(config => config.ClearProviders());

        services.AddSingleton<ISyncService, SyncService>();
        services.AddSingleton<IDataStorageService, DataStorageService>();
        services.AddSingleton<IDataStorageRepository, DataStorageRepository>();
        services.AddSingleton<IDistributionService, DistributionService>();

        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<IHealthService, HealthService>();
        services.AddSingleton<ITcpService, TcpService>();

        services.AddHostedService<BackgroundTask.BackgroundTask>();
        services.AddHostedService<BackgroundTask.HealthCheck>();
    }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.Run();
    }
}