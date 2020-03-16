using System.Threading.Tasks;

namespace Bacan.Core
{
    public interface IBacanHost
    {
        Task RunAsync();
        void Run();
    }
}