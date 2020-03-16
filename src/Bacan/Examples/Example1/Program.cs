using System.Threading.Tasks;

namespace Bacan.Examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = BacanHostBuilder.CreateDefaultHost(args);
            await host.RunAsync();
        }
    }
}