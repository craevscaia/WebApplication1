using Server.Models;

namespace Server.Repository;

public interface IDataStorageRepository
{
    public IDictionary<int, Data> GetAll();
    public KeyValuePair<int, Data> GetById(int id);
    public Task<Result> Save(int id, Data fileData);
}