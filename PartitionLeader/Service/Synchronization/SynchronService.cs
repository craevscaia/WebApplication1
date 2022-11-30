namespace PartitionLeader.Service.Synchronization;

public class SynchronService : ISynchronService
{
    public void SyncData(CancellationToken cancellationToken)
    {
        //sync all data between clusters
    }
}