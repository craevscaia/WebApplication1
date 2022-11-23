using ClientServer.Models;

namespace ClientServer.Repository;

public class DataStorageRepository : IDataStorageRepository
{
    private readonly IDictionary<int, Data> _storage;

    public DataStorageRepository()
    {
        _storage = new Dictionary<int, Data>();
    }

    public IDictionary<int, Data> GetAll()
    {
        return _storage;
    }

    public KeyValuePair<int, Data> GetById(int id)
    {
        return _storage.FirstOrDefault(s => s.Key == id);
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
}