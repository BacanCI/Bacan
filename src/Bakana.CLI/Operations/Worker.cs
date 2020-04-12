using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Worker : OperationBase<WorkerOptions>
    {
        public Worker(WorkerOptions options) : base(options)
        {
        }

        protected override Task Validate(WorkerOptions options)
        {
            return Task.CompletedTask;
        }

        protected override Task<int> Run(WorkerOptions options)
        {
            return Task.FromResult(10);
        }
    }
}
