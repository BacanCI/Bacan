using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core
{
    public interface IBakanaLoader
    {
        Task<string> LoadBatch(Batch batch);
    }
}