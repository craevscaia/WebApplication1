using ClientServer.Helpers;
using ClientServer.Models;
using ClientServer.Repositories;

namespace ClientServer.Services.DataService;

public class DataStorageService : IDataStorageService
{
    private readonly IDataStorageRepository _dataStorageRepository;

    public DataStorageService(IDataStorageRepository dataStorageRepository)
    {
        _dataStorageRepository = dataStorageRepository;
    }

    public async Task<KeyValuePair<int, Data>?> GetById(int id)
    {
        return await _dataStorageRepository.GetById(id);
    }

    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _dataStorageRepository.GetAll();
    }

    public async Task<Result> Save(Data data)
    {
        var id = IdGenerator.GenerateId();
        return await _dataStorageRepository.Save(id, data);
    }

    public Task<Data> Update(int id, Data data)
    {
        return _dataStorageRepository.Update(id, data);
    }

    public async Task<Result> Delete(int id)
    {
        return await _dataStorageRepository.Delete(id);
    }
}