using System.Text;
using Newtonsoft.Json;
using Server.Helpers;
using Server.Models;

namespace Server.Service;

public class HttpService : IHttpService
{
    public async Task<Result?> Save(Data fileData, string url)
    {
        try
        {
            var json = JsonConvert.SerializeObject(fileData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync($"{url}", content);
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<Result>(dataAsJson);
            
            PrintConsole.Write($"Saved data to url {url}", ConsoleColor.Green);

            return deserialized;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Failed save to {url}", ConsoleColor.DarkRed);
        }

        return null;
    }
}