using System.Linq;
using System.Threading.Tasks;
using Bakana.Options;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Operations.Arguments
{
    [TestFixture]
    public class ProducerArgumentsTests : ArgumentsTestFixtureBase<ProducerOptions>
    {
        [TestCaseSource(nameof(_argumentCases))]
        public async Task Producer_Options_Should_Be_Equivalent_To_CLI_Arguments(string cliArguments, int expectedExitCode, ProducerOptions expectedOptions)
        {
            await Assert_Options_Should_Be_Equivalent_To_CLI_Arguments(cliArguments, expectedExitCode, expectedOptions);
        }

        private static object[] _argumentCases =
        {
            new object[] { "PRODUCER", ExitCodes.InvalidArguments, null },
            new object[] { "producer", ExitCodes.InvalidArguments, null },
            new object[] { "producer START", ExitCodes.Success, new ProducerOptions
                {
                    Operation = ProducerOperation.Start
                } 
            },
            new object[] { "producer start", ExitCodes.Success, new ProducerOptions
                {
                    Operation = ProducerOperation.Start
                } 
            },
        };

        [Test]
        public async Task Test()
        {
            // Arrange
            var args = GetArgs("PRODUCER");

            // Act
            var result = await Runner.Run(args);
            
            result.Should().Be(ExitCodes.InvalidArguments);

            Runner.Errors.Count.Should().Be(1);
            var error = Runner.Errors.Single();
            error.Tag.Should().Be(ErrorType.BadVerbSelectedError);
        }
    }
}
