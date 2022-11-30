using ClientServer.Helpers;
using ClientServer.Setting;
using Newtonsoft.Json;

namespace ClientServer.Services.HealthService;

public class HealthService : IHealthService
{
    public async Task CheckHealth()
    {
        await Task.Delay(10000);

        //check if the partition leader is healthy
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

    private static async Task<bool> IsPartitionLeaderHealthy()
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