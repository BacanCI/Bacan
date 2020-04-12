using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Init : OperationBase<InitOptions>
    {
        public Init(InitOptions options) : base(options)
        {
        }

        protected override Task Validate(InitOptions options)
        {
            return Task.CompletedTask;
        }

        protected override Task<int> Run(InitOptions options)
        {
            return Task.FromResult(10);
        }
    }
}
