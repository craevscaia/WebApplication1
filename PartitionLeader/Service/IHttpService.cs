using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Models;

namespace PartitionLeader.Service;

public interface IHttpService
{
    public Task<Result?> SendToOtherServer([FromForm] Data fileData, string url);
}