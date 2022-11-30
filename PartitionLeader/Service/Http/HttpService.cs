using System.Text;
using Newtonsoft.Json;
using PartitionLeader.Helpers;
using PartitionLeader.Models;

namespace PartitionLeader.Service.Http;

public class HttpService : IHttpService
{
    public async Task<IDictionary<int, Data>?> GetAll(string url)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{url}/all");

            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<IDictionary<int, Data>>(dataAsJson);

            ConsoleHelper.Print($"Got data from url {url}", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            ConsoleHelper.Print($"Failed get from {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<KeyValuePair<int, Data>?> GetById(int id, string url)
    {
        try
        {
            var json = JsonConvert.SerializeObject(id);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.GetAsync($"{url}/get/{id}");

            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<KeyValuePair<int, Data>>(dataAsJson);

            ConsoleHelper.Print($"Got data from url with id: {id}", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            ConsoleHelper.Print($"Failed get by id: {id}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<Data?> Update(int id, Data data, string url)
    {
        try
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PutAsync($"{url}/update/{id}", content);

            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<Data>(dataAsJson);

            ConsoleHelper.Print($"Updated data from url {url}, id: {id}", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            ConsoleHelper.Print($"Failed to update id: {id} from {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<Result?> Save(Data data, string url)
    {
        try
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync($"{url}", content);

            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserializedResult = JsonConvert.DeserializeObject<Result>(dataAsJson);

            ConsoleHelper.Print($"Saved data to url {url}", ConsoleColor.Green);

            return deserializedResult;
        }
        catch (Exception e)
        {
            ConsoleHelper.Print($"Failed save to {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<IList<Result>?> Delete(int id, string url)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.DeleteAsync($"{url}/delete/{id}");

            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<IList<Result>>(dataAsJson);

            ConsoleHelper.Print($"Deleted data from url {url} with id: {id}", ConsoleColor.Green);

            return deserialized;
        }
        catch (Exception e)
        {
            ConsoleHelper.Print($"Failed to delete from {url} id: {id}", ConsoleColor.DarkRed);
        }

        return null;
    }
}