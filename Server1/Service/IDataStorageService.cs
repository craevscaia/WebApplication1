using ClientServer.Models;

namespace ClientServer.Service;

public interface IDataStorageService
{
    public IDictionary<int, Data> GetAll();
    public KeyValuePair<int, Data> GetById(int id);
    public Task<Result> Save(Data entity);
}