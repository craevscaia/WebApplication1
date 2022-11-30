using ClientServer.Repositories;
using ClientServer.Services.DataService;
using ClientServer.Services.DistributionService;
using ClientServer.Services.HealthService;
using ClientServer.Services.HttpService;
using ClientServer.Services.Sync;
using ClientServer.Services.TcpService;

namespace ClientServer;

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

        services.AddSingleton<IDataStorageRepository, DataStorageRepository>();

        services.AddSingleton<ISyncService, SyncService>();
        services.AddSingleton<IDataStorageService, DataStorageService>();
        services.AddSingleton<IDistributionService, DistributionService>();

        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<IHealthService, HealthService>();
        services.AddSingleton<ITcpService, TcpService>();

        services.AddHostedService<BackgroundTask.BackgroundTask>();
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