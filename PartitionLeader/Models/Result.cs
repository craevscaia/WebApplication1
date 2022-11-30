using PartitionLeader.Setting;

namespace PartitionLeader.Models;

public class Result
{
    public int StorageCount { get; set; }

    public int LastProcessedId { get; set; }

    public int Port { get; set; }

    public ServerName ServerName { get; set; }
    public bool IsAlive { get; set; }

    public Result()
    {
        Port = Settings.ThisPort;
        ServerName = Settings.ServerName;
        IsAlive = true;
    }
}