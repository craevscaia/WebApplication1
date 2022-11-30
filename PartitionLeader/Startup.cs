using PartitionLeader.Repository;
using PartitionLeader.Service;
using PartitionLeader.Service.DataStorage;
using PartitionLeader.Service.DistributionService;
using PartitionLeader.Service.HealthService;
using PartitionLeader.Service.Http;
using PartitionLeader.Service.Synchronization;
using PartitionLeader.Service.Tcp;

namespace PartitionLeader;

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
        services.AddSingleton<ISynchronService, SynchronService>();

        services.AddSingleton<IDataStorageService, DataStorageService>();
        services.AddSingleton<IDistributionService, DistributionService>();

        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<ITcpService, TcpService>();
        services.AddSingleton<IHealthService, HealthService>();
        
        services.AddHostedService<BackgroundTask.BackgroundTask>();;
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