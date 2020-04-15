using System.Linq;
using System.Threading.Tasks;
using Bakana.Options;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Operations.Arguments
{
    [TestFixture]
    public class WorkerArgumentsTests : ArgumentsTestFixtureBase<WorkerOptions>
    {
        [TestCaseSource(nameof(_argumentCases))]
        public async Task Worker_Options_Should_Be_Equivalent_To_CLI_Arguments(string cliArguments, int expectedExitCode, WorkerOptions expectedOptions)
        {
            await Assert_Options_Should_Be_Equivalent_To_CLI_Arguments(cliArguments, expectedExitCode, expectedOptions);
        }

        private static object[] _argumentCases =
        {
            new object[] { "WORKER", ExitCodes.InvalidArguments, null },
            new object[] { "worker", ExitCodes.InvalidArguments, null },
            new object[] { "worker START", ExitCodes.Success, new WorkerOptions
                {
                    Operation = WorkerOperation.Start
                } 
            },
            new object[] { "worker start", ExitCodes.Success, new WorkerOptions
                {
                    Operation = WorkerOperation.Start
                } 
            },
        };

        [Test]
        public async Task Test()
        {
            // Arrange
            var args = GetArgs("WORKER");

            // Act
            var result = await Runner.Run(args);
            
            result.Should().Be(ExitCodes.InvalidArguments);

            Runner.Errors.Count.Should().Be(1);
            var error = Runner.Errors.Single();
            error.Tag.Should().Be(ErrorType.BadVerbSelectedError);
        }
    }
}
