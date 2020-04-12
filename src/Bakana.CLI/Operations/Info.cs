using System.Threading.Tasks;
using Bakana.Options;

namespace Bakana.Operations
{
    public class Info : OperationBase<InfoOptions>
    {
        public Info(InfoOptions options) : base(options)
        {
        }

        protected override Task Validate(InfoOptions options)
        {
            return Task.CompletedTask;
        }

        protected override Task<int> Run(InfoOptions options)
        {
            return Task.FromResult(10);
        }
    }
}
