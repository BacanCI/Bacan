using System.Threading.Tasks;

namespace Bakana.Operations
{
    public abstract class OperationBase<T> : IOperation<T>
    {
        protected T Options { get; private set; }
        
        public async Task<int> Run(T options)
        {
            Options = options;
            await Validate();
            return await Run();
        }

        protected abstract Task Validate();

        protected abstract Task<int> Run();
    }
}