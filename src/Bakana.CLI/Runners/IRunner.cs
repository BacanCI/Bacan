using System.Threading.Tasks;

namespace Bakana.Runners
{
    public interface IRunner
    {
        Task Run(IOptions options);
    }
}