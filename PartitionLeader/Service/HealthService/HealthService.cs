using Newtonsoft.Json;
using PartitionLeader.Helpers;
using PartitionLeader.Setting;

namespace PartitionLeader.Service.HealthService;

public class HealthService : IHealthService
{
    public async Task CheckHealth()
    {
        await Task.Delay(10000);
        
        try
        {
            while (true)
            {
                await Task.Delay(10000);
                var server1Health = await IsServerHealthy(Settings.Server1);
                var server2Health = await IsServerHealthy(Settings.Server2);

                if (!server1Health)
                {
                    StorageHelper.server1Status.SetServerStatus(server1Health);
                    ConsoleHelper.Print($"Server 1 is down", ConsoleColor.Red);
                    break;
                }
                if (!server2Health)
                {
                    StorageHelper.server2Status.SetServerStatus(server2Health);
                    ConsoleHelper.Print($"Server 2 is down", ConsoleColor.Red);
                    break;
                }
            }
        }
        catch (Exception e)
        {
            ConsoleHelper.Print($"Health check failed", ConsoleColor.DarkRed);
        }
    }
    
    private static async Task<bool> IsServerHealthy(string url)
    {
        try
        {
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