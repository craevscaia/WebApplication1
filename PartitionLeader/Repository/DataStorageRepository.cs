using PartitionLeader.Models;

namespace PartitionLeader.Repository;

public class DataStorageRepository : IDataStorageRepository
{
    private readonly IDictionary<int, Data> _storage;

    public DataStorageRepository()
    {
        _storage = new Dictionary<int, Data>();
    }

    public Task<IDictionary<int, Data>> GetAll()
    {
        return Task.FromResult(_storage);
    }

    public Task<KeyValuePair<int, Data>> GetById(int id)
    {
        return Task.FromResult(_storage.FirstOrDefault(s => s.Key == id));
    }

    public Task<Result> Save(int id, Data fileData)
    {
        _storage.Add(id, fileData);
        return Task.FromResult(new Result
        {
            StorageCount = _storage.Count,
            LastProcessedId = id
        });
    }

    public Task<Data> Update(int id, Data data)
    {
        return Task.FromResult(_storage[id] = data);
    }

    public Task<Result> Delete(int id) //100 , sterge Id = 10 -> raman 99 din 100 fiesiere, ai atins ultimul id 10
    {
        _storage.Remove(id);
        
        return Task.FromResult(new Result()
        {
            StorageCount = _storage.Count,
            LastProcessedId = id
        });
    }
}