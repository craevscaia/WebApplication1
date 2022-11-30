using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Services.HttpService;

public interface IHttpService
{
    public Task<IDictionary<int, Data>?> GetAll(string url);
    public Task<KeyValuePair<int, Data>?> GetById(int id, string url);
    public Task<Data?> Update(int id, [FromForm] Data data, string url);
    public Task<Result?> Save([FromForm] Data data, string url);
    public Task<Result?> Delete([FromRoute] int id, string url);
}