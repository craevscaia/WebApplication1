using PartitionLeader.Models;

namespace PartitionLeader.Service;

public interface IDataStorageService
{
    public IDictionary<int, Data> GetAll();
    public KeyValuePair<int, Data> GetById(int id);
    public Task<Data> Update(int id, Data data);
    public Task<Result> Delete(int id);
    public Task<Result> Save(int id, Data entity);
}