using Server.Models;

namespace Server.Services.TcpService;

public interface ITcpService
{
    public Task RunTcp();
    public Result? TcpSave(Data data, int serverPort);
}