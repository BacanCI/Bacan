using System.Threading.Tasks;

namespace Bakana.Operations
{
    public interface IOperation<T>
    {
        Task<int> Run(T options);
    }
}