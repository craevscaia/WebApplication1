﻿using Server.Models;
using Server.Setting;

namespace Server.Helpers;

public static class StorageHelper
{
    private static Result _partitionLeaderStatus = new(){
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.ThisPort,
        ServerName = Settings.ServerName
    };

    private static Result _server1Status = new(){
        StorageCount = 0,
        LastProcessedId = 0
    };

    private static Result _server2Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };
    
    public static string GetOptimalServerUrl()
    {
        var optimalServer = _partitionLeaderStatus;
        if (optimalServer.StorageCount > _server1Status.StorageCount)
        {
            optimalServer = _server1Status;
        }
        
        if (optimalServer.StorageCount > _server2Status.StorageCount)
        {
            optimalServer = _server2Status;
        }

        return $"{Settings.BaseUrl}{optimalServer.Port}";
    }
    
    public static List<ServerName> GetOptimalServers()
    {
        var servers = new List<ServerName>();

        var optimalServer1 = _partitionLeaderStatus;
        var optimalServer2 = _server1Status;

        if (_server2Status.StorageCount < optimalServer1.StorageCount)
        {
            optimalServer1 = _server2Status;
        }
        else if (_server2Status.StorageCount < optimalServer2.StorageCount)
        {
            optimalServer2 = _server2Status;
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
                    _server1Status = summary;
                    break;
                case ServerName.Server2:
                    _server2Status = summary;
                    break;
            }
        }
    }

    public static Result? GetStatus()
    {
        return _server1Status;
    }
}