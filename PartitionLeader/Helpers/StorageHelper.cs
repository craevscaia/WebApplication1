using PartitionLeader.Models;
using PartitionLeader.Setting;

namespace PartitionLeader.Helpers;

public static class StorageHelper
{
     private static Result _partitionLeaderStatus = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.ThisPort,
        ServerName = Settings.ServerName
    };

     public static Result server1Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.Server1Port,
        ServerName = ServerName.Server1
    };

     public static Result server2Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.Server2Port,
        ServerName = ServerName.Server2
    };
    
    public static void SetServerStatus(this Result result, bool status)
    {
        result.IsAlive = status;
    }

    public static string GetOptimalServerUrl()
    {
        var optimalServer = _partitionLeaderStatus;
        if (optimalServer.StorageCount > server1Status.StorageCount)
        {
            optimalServer = server1Status;
        }

        if (optimalServer.StorageCount > server2Status.StorageCount)
        {
            optimalServer = server2Status;
        }

        return $"{Settings.BaseUrl}{optimalServer.Port}";
    }

    public static List<ServerName> GetOptimalServers()
    {
        var servers = new List<ServerName>();

        var optimalServer1 = _partitionLeaderStatus;
        var optimalServer2 = server1Status;

        if (server2Status.StorageCount < optimalServer2.StorageCount)
        {
            optimalServer1 = server2Status;
        }
        else if (server2Status.StorageCount < optimalServer1.StorageCount)
        {
            optimalServer2 = server2Status;
        }

        servers.Add(optimalServer1.ServerName);
        servers.Add(optimalServer2.ServerName);

        return servers;
    }

    public static void UpdateServerStatus(this Result? summary)
    {
        if (summary != null)
        {
            switch (summary.ServerName)
            {
                case ServerName.PartitionLeader:
                    _partitionLeaderStatus = summary;
                    break;
                case ServerName.Server1:
                    server1Status = summary;
                    break;
                case ServerName.Server2:
                    server2Status = summary;
                    break;
            }
        }
    }

    public static Result? GetStatus()
    {
        return _partitionLeaderStatus;
    }

    public static IList<Result>? GetStatusFromServers()
    {
        var statuses = new List<Result>();
        statuses.Add(server1Status);
        statuses.Add(server2Status);
        statuses.Add(_partitionLeaderStatus);
        return statuses;
    }
}