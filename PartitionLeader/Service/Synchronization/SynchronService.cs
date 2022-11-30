using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Service.DataStorage;
using PartitionLeader.Service.Http;
using PartitionLeader.Setting;

namespace PartitionLeader.Service.Synchronization;

public class SynchronService : ISynchronService
{
    private readonly IHttpService _httpService;
    private readonly IDataStorageService _dataService;

    public SynchronService(IHttpService httpService, IDataStorageService dataService)
    {
        _httpService = httpService;
        _dataService = dataService;
    }

    public async Task SyncData(CancellationToken cancellationToken)
    {
        while (true)
        {
            await Task.Delay(10000, cancellationToken);
            var serverData = await _dataService.GetAll();
            if (serverData != null)
            {
                foreach (var data in serverData)
                {
                    await CheckDataBackup(data);
                }
            }

            ConsoleHelper.Print($"All data has a reserve copy", ConsoleColor.Green);
        }
    }

    private async Task CheckDataBackup(KeyValuePair<int, Data> data)
    {
        var backupServer1 = await _httpService.GetById(data.Key, Settings.Server1);
        var backupServer2 = await _httpService.GetById(data.Key, Settings.Server2);

        await GetFeedback(data, backupServer1, backupServer2);
    }

    private async Task GetFeedback(KeyValuePair<int, Data> data, KeyValuePair<int, Data>? backupServer1,
        KeyValuePair<int, Data>? backupServer2)
    {
        if (backupServer1?.Value == null && backupServer2?.Value == null)
        {
            var optimalServerUrl = StorageHelper.GetOptimalServerUrl();
            ConsoleHelper.Print(
                $"No data backup found for id: {data.Key}. Creating a backup on server: {optimalServerUrl}",
                ConsoleColor.Red);

            var result = await _httpService.Save(data.Value, optimalServerUrl);

            result?.UpdateServerStatus();
        }
    }
}