using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Models;

namespace PartitionLeader.Service;

public interface IHttpService
{
    public Task<Result?> Save([FromForm] Data fileData, string url);
}