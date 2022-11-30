﻿using ClientServer.Models;

namespace ClientServer.Setting;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.Server1;
    
    public static bool Leader = false;
    
    public static readonly bool InDocker = false; // set to false when running on localhost
    
    public static readonly string ServerIp = InDocker ? "host.docker.internal" : "localhost";

    public static readonly int LeaderPort = 7112;
    public static readonly int Server1Port = 7173;
    public static readonly int Server2Port = 7156;
    
    public static readonly int Server1TcpSavePort = 8081;
    public static readonly int Server2TcpSavePort = 8082;
    
    public static readonly int ThisPort = Server1Port;
    
    public static readonly string BaseUrl = $"https://{ServerIp}:"; //local

    public static readonly string ThisServerUrl = $"https://{ServerIp}:{ThisPort}"; //docker

    public static readonly string PartitionLeader = $"https://{ServerIp}:{LeaderPort}"; //local
    public static readonly string Server1 = $"https://{ServerIp}:{Server1Port}"; //local
    public static readonly string Server2 = $"https://{ServerIp}:{Server2Port}"; //local
}