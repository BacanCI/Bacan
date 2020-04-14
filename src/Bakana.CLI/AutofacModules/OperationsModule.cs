using Autofac;
using Bakana.Operations;

namespace Bakana.AutofacModules
{
    public class OperationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IOperation<>));
            
            builder.RegisterType<OperationFactory>()
                .AsImplementedInterfaces();
        }
    }
}