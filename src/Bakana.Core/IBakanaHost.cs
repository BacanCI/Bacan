using System.Threading.Tasks;

namespace Bakana.Core
{
    public interface IBakanaHost
    {
        Task RunAsync();
        void Run();
    }
}