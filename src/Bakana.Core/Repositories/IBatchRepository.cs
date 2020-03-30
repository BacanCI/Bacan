using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface IBatchRepository
    {
        Task Create(Batch batch);
        Task Update(Batch batch);
        Task Delete(string batchId);
        Task<Batch> Get(string batchId);
    }
}