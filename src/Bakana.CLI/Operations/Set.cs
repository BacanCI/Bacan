using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Set : OperationBase<SetOptions>
    {
        public Set(SetOptions options) : base(options)
        {
        }

        protected override Task Validate(SetOptions options)
        {
            return Task.CompletedTask;
        }

        protected override Task<int> Run(SetOptions options)
        {
            return Task.FromResult(10);
        }
    }
}
