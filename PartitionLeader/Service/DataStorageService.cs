using PartitionLeader.Models;
using PartitionLeader.Repository;

namespace PartitionLeader.Service;

public class DataStorageService : IDataStorageService
{
    private readonly IDataStorageRepository _dataStorageRepository;

    public DataStorageService(IDataStorageRepository dataStorageRepository)
    {
        _dataStorageRepository = dataStorageRepository;
    }

    public IDictionary<int, Data> GetAll()
    {
        return _dataStorageRepository.GetAll();
    }

    public KeyValuePair<int, Data> GetById(int id)
    {
        return _dataStorageRepository.GetById(id);
    }

    public Task<Data> Update(int id, Data data)
    {
        return _dataStorageRepository.Update(id, data);
    }

    public Task<Result> Delete(int id)
    {
        return _dataStorageRepository.Delete(id);
    }

    public Task<Result> Save(int id, Data fileData)
    {
        return _dataStorageRepository.Save(id, fileData);
    }
}