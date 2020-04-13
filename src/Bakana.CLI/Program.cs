using System;
using System.Threading.Tasks;
using Autofac;
using Bakana.AutofacModules;

namespace Bakana
{
    public class Program 
    {
        static async Task Main(string[] args)
        {
            args = new[]
            {
                "batch",
                "123",
                "Upload",
                "abc.zip",
                "--name",
                "def.zip"
            };

            using var container = GetContainer();
            var runner = container.Resolve<IConsoleRunner>();
                
            var result = await runner.Run(args); 

            Environment.ExitCode = result;
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<OperationsModule>();

            builder.RegisterType<ConsoleRunner>()
                .AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
