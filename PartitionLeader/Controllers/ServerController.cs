using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Service;

namespace PartitionLeader.Controllers;

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
    public async Task<Result> Save([FromForm] UploadedFile uploadedFile)
    {
        var fileData = uploadedFile.MapData(); //converting file to our model
        var result = await _dataStorageService.Save(fileData.Id, fileData);

        var server1Result = await _httpService.SendToOtherServer(fileData, Settings.Settings.Server1);
        // var server2Result = await _httpService.Save(fileData, Settings.Settings.Server2);

        // server1Result?.UpdateStatus();
        // result.UpdateStatus();
        return result;
    }

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromBody] Data data)
    {
        return await _dataStorageService.Update(id, data);
    }

    [HttpDelete("/delete/{id}")]
    public async Task<Result> Delete([FromRoute] int id, [FromBody] Data data)
    {
        await _dataStorageService.Delete(id);
        return new Result()
        {
            StorageCount = _dataStorageService.GetAll().Count
        };
    }
}