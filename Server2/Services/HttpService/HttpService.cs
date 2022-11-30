﻿using System.Text;
using Newtonsoft.Json;
using Server.Models;

namespace Server.Services.HttpService;

public class HttpService : IHttpService
{
   public async Task<KeyValuePair<int, Data>?> GetById(int id, string url)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{url}/get/{id}");
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<KeyValuePair<int, Data>>(dataAsJson);
            
            Console.Write($"Got data from url with id: {id}", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            Console.Write($"Failed get by id: {id}", ConsoleColor.DarkRed);
        }

        return null;
    }
    

    public async Task<IDictionary<int, Data>?> GetAll(string url)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{url}/all");
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<IDictionary<int, Data>>(dataAsJson);
            
            Console.Write($"Got data from url {url}", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            Console.Write($"Failed get from {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<Data> Update(int id, Data data, string url)
    {
        try
        {
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PutAsync($"{url}/update/{id}" , content);
            Console.Write($"Updated data from url {url}, id: {id}", ConsoleColor.Green);
            return null;
        }
        catch (Exception e)
        {
            Console.Write($"Failed to update id: {id} from {url}", ConsoleColor.DarkRed);
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
            var deserialized = JsonConvert.DeserializeObject<Result>(dataAsJson);
            
            Console.Write($"Saved data to url {url}", ConsoleColor.Green);

            return deserialized;
        }
        catch (Exception e)
        {
            Console.Write($"Failed save to {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<Result?> Delete(int id, string url)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.DeleteAsync($"{url}/delete/{id}");
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<Result>(dataAsJson);
            
            Console.Write($"Deleted data from url {url} with id: {id}", ConsoleColor.Green);

            return deserialized;
        }
        catch (Exception e)
        {
            Console.Write($"Failed to delete from {url} id: {id}", ConsoleColor.DarkRed);
        }

        return null;
    }
}