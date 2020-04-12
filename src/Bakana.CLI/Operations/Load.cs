using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Load : OperationBase<LoadOptions>
    {
        public Load(LoadOptions options) : base(options)
        {
        }

        protected override Task Validate(LoadOptions options)
        {
            return Task.CompletedTask;
        }

        protected override Task<int> Run(LoadOptions options)
        {
            return Task.FromResult(10);
        }
    }
}
