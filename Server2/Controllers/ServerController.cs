using Server.Models;
using Server.Service;

namespace Server.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDataStorageService _dataStorageService;
    private readonly IHttpService _httpService;

    public ServerController(IDataStorageService dataStorageService, IHttpService httpService)
    {
        _dataStorageService = dataStorageService;
        _httpService = httpService;
    }

    [HttpGet("/all")]
    public async Task<ICollection<int>> GetAll()
    {
        return await Task.FromResult(_dataStorageService.GetAll().Keys);
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>> GetById([FromRoute] int id)
    {
        return await Task.FromResult(_dataStorageService.GetById(id));
    }

    [HttpPost]
    public async Task<Result> Save([FromBody] Data data)
    {
        await _dataStorageService.Save(data);
        //TODO : Add result to the list
        //TODO : Count storage, and generate proper results
        Console.WriteLine($"I recieved file with name {data.FileName}");
        return new Result()
        {
            ServerName = ServerName.Server1,
            StorageCount = _dataStorageService.GetAll().Count,
            LastProcessedId = data.Id
        };
    }
    
}