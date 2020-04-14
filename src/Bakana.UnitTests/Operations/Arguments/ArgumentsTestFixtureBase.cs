using System.Threading.Tasks;
using Autofac;
using Bakana.Operations;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Operations.Arguments
{
    public class ArgumentsTestFixtureBase<T>
    {
        protected IContainer Container { get; private set; }
        protected MockOperation<T> MockOperation { get; private set; }
        protected IConsoleRunner Runner { get; private set; }
        
        protected static string[] GetArgs(string cliArguments)
        {
            return cliArguments.Split(' ');
        }

        protected async Task Assert_Options_Should_Be_Equivalent_To_CLI_Arguments(string cliArguments, int expectedExitCode, T expectedOptions)
        {
            var args = GetArgs(cliArguments);

            var result = await Runner.Run(args);
            
            result.Should().Be(expectedExitCode);

            MockOperation.Options.Should().BeEquivalentTo(expectedOptions);
        }

        [SetUp]
        protected void Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MockOperation<T>>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<OperationFactory>().AsImplementedInterfaces();
            builder.RegisterType<ConsoleRunner>().AsImplementedInterfaces();

            Container = builder.Build();

            MockOperation = Container.Resolve<MockOperation<T>>();
            Runner = Container.Resolve<IConsoleRunner>();
        }

        [TearDown]
        protected void TearDown()
        {
            Container.Dispose();
        }
    }

    public class MockOperation<T> : IOperation<T>
    {
        public T Options { get; set; }
        
        public Task<int> Run(T options)
        {
            Options = options;
            
            return Task.FromResult(ExitCodes.Success);
        }
    }
}
