namespace PartitionLeader.Service.Synchronization;

public interface ISynchronService
{
    public Task SyncData(CancellationToken cancellationToken);
}