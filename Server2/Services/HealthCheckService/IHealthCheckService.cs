namespace Server.Services.HealthCheckService;

public interface IHealthCheckService
{
    Task CheckHealth();
}