using PartitionLeader.Models;

namespace PartitionLeader.Service.Tcp;

public interface ITcpService
{
    public Result? TcpSave(Data data, int serverPort);
}