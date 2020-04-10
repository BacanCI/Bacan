using System.Threading.Tasks;

namespace Bakana.Loader
{
    public interface IBatchLoader
    {
        Task<string> LoadBatch(string path);
    }
}