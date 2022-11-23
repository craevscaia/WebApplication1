using Server.Models;

namespace Server.Service;

public interface IDataStorageService
{
    public IDictionary<int, Data> GetAll();
    public KeyValuePair<int, Data> GetById(int id);
    public Task<Result> Save(Data entity);
}