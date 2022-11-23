using ClientServer.Models;

namespace ClientServer.Settings;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.PartitionLeader;
    
    public static readonly bool Leader = true;
    public static readonly string ServerIP = "localhost";  
    public static readonly int Port = 7172;
    public static readonly string BaseUrl = $"https://localhost:"; //local

    public static readonly string ThisServerUrl = $"https://localhost:{7172}"; //docker

    public static readonly string PartitionLeader = $"https://localhost:{7172}"; //local
    public static readonly string Server1 = $"https://localhost:{7095}"; //local
    public static readonly string Server2 = $"https://localhost:{7046}"; //local
}