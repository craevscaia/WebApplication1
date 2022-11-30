using Server.Services.HealthCheckService;

namespace Server.BackgroundTask;

public class HealthCheck : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HealthCheck(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000, stoppingToken);
        using var scope = _serviceScopeFactory.CreateScope();
        var scoped = scope.ServiceProvider.GetRequiredService<IHealthCheckService>();
        await scoped.CheckHealth();
    }
}