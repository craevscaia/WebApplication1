using ClientServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientServer.Service;

public interface IHttpService
{
    public Task<Result?> Save([FromForm] Data fileData, string url);
}