using System.Threading.Tasks;
using Bakana.Core;
using Microsoft.Extensions.Hosting;

namespace Bakana
{
    public class BakanaHost : IBakanaHost
    {
        private readonly IHost host;
        
        internal BakanaHost(IHost host)
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