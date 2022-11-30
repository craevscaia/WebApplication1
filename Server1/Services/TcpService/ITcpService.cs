using ClientServer.Models;

namespace ClientServer.Services.TcpService;

public interface ITcpService
{
    public Task RunTcp();
    public Result? TcpSave(Data data, int serverPort);
}