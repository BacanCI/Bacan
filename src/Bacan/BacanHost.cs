using System.Threading.Tasks;
using Bacan.Core;
using Microsoft.Extensions.Hosting;

namespace Bacan
{
    public class BacanHost : IBacanHost
    {
        private readonly IHost host;
        
        internal BacanHost(IHost host)
        {
            this.host = host;
        }

        public async Task RunAsync()
        {
            await host.RunAsync();
        }

        public void Run()
        {
            host.Run();
        }
    }
}