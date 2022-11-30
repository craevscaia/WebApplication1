using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Models;

namespace PartitionLeader.Service.Http;

public interface IHttpService
{
    public Task<IDictionary<int, Data>?> GetAll(string url);
    public Task<KeyValuePair<int, Data>?> GetById(int id, string url);
    public Task<Data?> Update(int id, [FromForm] Data data, string url);
    public Task<Result?> Save([FromForm] Data data, string url);
    public Task<IList<Result>?> Delete([FromRoute] int id, string url);
}