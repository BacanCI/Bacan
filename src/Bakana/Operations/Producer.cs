using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Producer : OperationBase<ProducerOptions>
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
