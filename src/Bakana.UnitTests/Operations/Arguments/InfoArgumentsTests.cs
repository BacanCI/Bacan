using System.Linq;
using System.Threading.Tasks;
using Bakana.Options;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Operations.Arguments
{
    [TestFixture]
    public class InfoArgumentsTests : ArgumentsTestFixtureBase<InfoOptions>
    {
        [TestCaseSource(nameof(_argumentCases))]
        public async Task Info_Options_Should_Be_Equivalent_To_CLI_Arguments(string cliArguments, int expectedExitCode, InfoOptions expectedOptions)
        {
            await Assert_Options_Should_Be_Equivalent_To_CLI_Arguments(cliArguments, expectedExitCode, expectedOptions);
        }

        private static object[] _argumentCases =
        {
            new object[] { "INFO", ExitCodes.InvalidArguments, null },
            new object[] { "info", ExitCodes.Success, new InfoOptions() },
        };

        [Test]
        public async Task Test()
        {
            // Arrange
            var args = GetArgs("INFO");

            // Act
            var result = await Runner.Run(args);
            
            result.Should().Be(ExitCodes.InvalidArguments);

            Runner.Errors.Count.Should().Be(1);
            var error = Runner.Errors.Single();
            error.Tag.Should().Be(ErrorType.BadVerbSelectedError);
        }
    }
}
