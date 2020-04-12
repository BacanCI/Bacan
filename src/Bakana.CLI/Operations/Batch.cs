using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Batch : OperationBase<BatchOptions>
    {
        public Batch(BatchOptions options) : base(options)
        {
        }

        protected override Task Validate(BatchOptions options)
        {
            return Task.CompletedTask;
        }

        protected override Task<int> Run(BatchOptions options)
        {
            return Task.FromResult(10);
        }
    }
}
