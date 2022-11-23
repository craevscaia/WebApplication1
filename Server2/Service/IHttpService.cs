using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Service;

public interface IHttpService
{
    public Task<Result?> Save([FromForm] Data fileData, string url);
}