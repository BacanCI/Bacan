using System.Threading.Tasks;

namespace Bakana.Operations
{
    public abstract class OperationBase<T> : IOperation
    {
        protected T Options { get; }

        protected OperationBase(T options)
        {
            Options = options;
        }
        
        public async Task<int> Run()
        {
            await Validate(Options);
            return await Run(Options);
        }

        protected abstract Task Validate(T options);

        protected abstract Task<int> Run(T options);
    }
}