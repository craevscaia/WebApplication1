namespace PartitionLeader.Service.Synchronization;

public interface ISynchronService
{
    public void SyncData(CancellationToken cancellationToken)
    {
        //sync all data between clusters
    }
}