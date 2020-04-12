using System.Threading.Tasks;

namespace Bakana.Runners
{
    public abstract class RunnerBase : IRunner
    {
        protected IOptions Options { get; private set; }
        
        public async Task Run(IOptions options)
        {
            Options = options;

            await Validate();
            await Run();
        }

        protected abstract Task Validate();

        protected abstract Task Run();
    }
}