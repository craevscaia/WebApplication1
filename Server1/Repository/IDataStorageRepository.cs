using ClientServer.Models;

namespace ClientServer.Repository;

public interface IDataStorageRepository
{
    public IDictionary<int, Data> GetAll();
    public KeyValuePair<int, Data> GetById(int id);
    public Task<Result> Save(int id, Data fileData);
}