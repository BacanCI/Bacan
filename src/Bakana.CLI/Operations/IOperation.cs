using System.Threading.Tasks;

namespace Bakana.Operations
{
    public interface IOperation
    {
        Task<int> Run();
    }
}