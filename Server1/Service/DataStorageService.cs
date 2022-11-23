using ClientServer.Helpers;
using ClientServer.Models;
using ClientServer.Repository;

namespace ClientServer.Service;

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

    public Task<Result> Save(Data fileData)
    {
        var id = IdGenerator.GenerateId();
        return _dataStorageRepository.Save(id, fileData);
    }
}