using System.Threading.Tasks;

namespace Bakana.Examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = BakanaHostBuilder.CreateDefaultHost(args);
            await host.RunAsync();
        }
    }
}