using Microsoft.Extensions.Configuration;
using ServiceStack;

namespace Bacan
{
    public class BacanStartupActivator : ModularStartupActivator
    {
        public BacanStartupActivator(IConfiguration configuration) : base(configuration)
        {
            
        }
    }
}
