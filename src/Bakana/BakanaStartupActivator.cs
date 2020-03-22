using Microsoft.Extensions.Configuration;
using ServiceStack;

namespace Bakana
{
    public class BakanaStartupActivator : ModularStartupActivator
    {
        public BakanaStartupActivator(IConfiguration configuration) : base(configuration)
        {
            
        }
    }
}
