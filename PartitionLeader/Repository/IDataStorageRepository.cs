using PartitionLeader.Models;

namespace PartitionLeader.Repository;

public interface IDataStorageRepository
{
    public Task<IDictionary<int, Data>> GetAll();
    public Task<KeyValuePair<int, Data>> GetById(int id);
    public Task<Result> Save(int id, Data entity);
    public Task<Data> Update(int id, Data entity);
    public Task<Result> Delete(int id);
}