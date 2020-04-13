using Autofac;

namespace Bakana.Operations
{
    public interface IOperationFactory
    {
        IOperation<T> Create<T>();
    }

    public class OperationFactory : IOperationFactory
    {
        private readonly IComponentContext componentContext;

        public OperationFactory(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public IOperation<T> Create<T>()
        {
            return componentContext.Resolve<IOperation<T>>();
        }
    }
}