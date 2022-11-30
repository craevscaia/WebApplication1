using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Service.DistributionService;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDistributionService _distributionService;

    public ServerController(IDistributionService distributionService)
    {
        _distributionService = distributionService;
    }

    [HttpGet("/check")]
    public Task<bool> CheckStatus()
    {
        return Task.FromResult(true);
    }

    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _distributionService.GetAll();
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>?> GetById([FromRoute] int id)
    {
        return await _distributionService.GetById(id);
    }

    [HttpGet("/summary")]
    public async Task<IList<Result>?> GetSummary()
    {
        return await Task.FromResult(StorageHelper.GetStatusFromServers());
    }

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        var data = dataModel.MapData();

        var updateResult = await _distributionService.Update(id, data);

        return updateResult;
    }

    [HttpPost]
    public async Task<IList<Result>> Save([FromForm] DataModel dataModel)
    {
        var data = dataModel.MapData();

        IList<Result> resultSummaries = new List<Result>();
        try
        {
            resultSummaries = await _distributionService.Save(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return resultSummaries;
    }

    [HttpDelete("/delete/{id}")]
    public async Task<IList<Result>> Delete([FromRoute] int id)
    {
        return await _distributionService.Delete(id);
    }
}