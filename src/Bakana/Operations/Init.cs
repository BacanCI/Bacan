using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Init : OperationBase<InitOptions>
    {
        protected override Task Validate()
        {
            return Task.CompletedTask;
        }

        protected override Task<int> Run()
        {
            return Task.FromResult(10);
        }
    }
}
