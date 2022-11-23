using Server.Models;

namespace Server.Repository;

public interface IDataStorageRepository
{
    public IDictionary<int, Data> GetAll();
    public KeyValuePair<int, Data> GetById(int id);
    public Task<Result> Save(int id, Data fileData);
    Task<Data> Update(int id, Data data);
    Task<Result> Delete(int id);
}