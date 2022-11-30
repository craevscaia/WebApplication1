using Server.Models;

namespace Server.Services.DistributionService;

public interface IDistributionService
{
    public Task<KeyValuePair<int, Data>?> GetById(int id);
    public Task<IDictionary<int, Data>?> GetAll();
    public Task<Data> Update(int id, Data data);
    public Task<IList<Result>> Save(Data data);
    public Task<IList<Result>> Delete(int id);
}