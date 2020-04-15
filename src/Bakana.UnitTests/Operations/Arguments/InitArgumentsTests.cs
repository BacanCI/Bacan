using System.Linq;
using System.Threading.Tasks;
using Bakana.Options;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Operations.Arguments
{
    [TestFixture]
    public class InitArgumentsTests : ArgumentsTestFixtureBase<InitOptions>
    {
        [TestCaseSource(nameof(_argumentCases))]
        public async Task Init_Options_Should_Be_Equivalent_To_CLI_Arguments(string cliArguments, int expectedExitCode, InitOptions expectedOptions)
        {
            await Assert_Options_Should_Be_Equivalent_To_CLI_Arguments(cliArguments, expectedExitCode, expectedOptions);
        }

        private static object[] _argumentCases =
        {
            new object[] { "INIT", ExitCodes.InvalidArguments, null },
            new object[] { "init", ExitCodes.Success, new InitOptions() },
        };

        [Test]
        public async Task Test()
        {
            // Arrange
            var args = GetArgs("INIT");

            // Act
            var result = await Runner.Run(args);
            
            result.Should().Be(ExitCodes.InvalidArguments);

            Runner.Errors.Count.Should().Be(1);
            var error = Runner.Errors.Single();
            error.Tag.Should().Be(ErrorType.BadVerbSelectedError);
        }
    }
}
