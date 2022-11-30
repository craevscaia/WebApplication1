using Microsoft.AspNetCore.Mvc;
using Server.Helpers;
using Server.Helpers.Mappers;
using Server.Models;
using Server.Services.DistributionService;
using Server.Setting;

namespace Server.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDistributionService _distributionService;

    public ServerController(IDistributionService distributionService)
    {
        _distributionService = distributionService;
    }


    [HttpGet("/summary")]
    public async Task<Result?> GetSummary()
    {
        return await Task.FromResult(StorageHelper.GetStatus());
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

    [HttpPost]
    public async Task<IList<Result>> Save([FromBody] Data data)
    {
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

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromBody] Data data)
    {
        return await _distributionService.Update(id, data);
    }


    [HttpDelete("/delete/{id}")]
    public async Task<IList<Result>> Delete([FromRoute] int id)
    {
        return await _distributionService.Delete(id);
    }


    [HttpPost("/form/save")]
    public async Task<IList<Result>> Save([FromForm] DataModel dataModel)
    {
        if (!Settings.Leader)
        {
            return null;
        }
        
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

    [HttpPut("/form/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        if (!Settings.Leader)
        {
            return null;
        }
        
        var data = dataModel.MapData();

        var updateResult = await _distributionService.Update(id, data);

        return updateResult;
    }
}