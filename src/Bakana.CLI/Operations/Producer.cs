using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Producer : OperationBase<ProducerOptions>
    {
        public Producer(ProducerOptions options) : base(options)
        {
        }

        protected override Task Validate(ProducerOptions options)
        {
            return Task.CompletedTask;
        }

        protected override Task<int> Run(ProducerOptions options)
        {
            return Task.FromResult(10);
        }
    }
}
