using Newtonsoft.Json;
using Server.Helpers;
using Server.Services.HttpService;
using Server.Setting;

namespace Server.Services.HealthCheckService;

public class HealthService : IHealthCheckService
{
    public async Task CheckHealth()
    {
        await Task.Delay(10000);
        
        try
        {
            while (await IsPartitionLeaderHealthy())
            {
                await Task.Delay(10000);
            }
            ConsoleHelper.Print($"Server 1 is leader now", ConsoleColor.Green);
            Settings.Leader = true;
        }
        catch (Exception e)
        {
            ConsoleHelper.Print($"Partition leader check failed. Server 1 is leader now", ConsoleColor.DarkRed);
            Settings.Leader = true;
        }
    }

    private async Task<bool> IsPartitionLeaderHealthy()
    {
        try
        {
            var url = Settings.PartitionLeader;

            using var client = new HttpClient();
            
            var response = await client.GetAsync($"{url}/check");
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<bool>(dataAsJson);
            
            ConsoleHelper.Print($"Partition leader is healthy", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            ConsoleHelper.Print($"Partition leader check failed. Reassigning leader...", ConsoleColor.DarkRed);
            return false;
        }
    }
}