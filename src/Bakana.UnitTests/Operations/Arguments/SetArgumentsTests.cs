using System.Linq;
using System.Threading.Tasks;
using Bakana.Options;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Operations.Arguments
{
    [TestFixture]
    public class SetArgumentsTests : ArgumentsTestFixtureBase<SetOptions>
    {
        [TestCaseSource(nameof(_argumentCases))]
        public async Task Set_Options_Should_Be_Equivalent_To_CLI_Arguments(string cliArguments, int expectedExitCode, SetOptions expectedOptions)
        {
            await Assert_Options_Should_Be_Equivalent_To_CLI_Arguments(cliArguments, expectedExitCode, expectedOptions);
        }

        private static object[] _argumentCases =
        {
            new object[] { "SET", ExitCodes.InvalidArguments, null },
            new object[] { "set", ExitCodes.InvalidArguments, null },
            new object[] { "set A", ExitCodes.InvalidArguments, null },
            new object[] { "set A A1", ExitCodes.Success, new SetOptions
                {
                    Key = "A",
                    Value = "A1"
                } 
            },
        };

        [Test]
        public async Task Test()
        {
            // Arrange
            var args = GetArgs("SET");

            // Act
            var result = await Runner.Run(args);
            
            result.Should().Be(ExitCodes.InvalidArguments);

            Runner.Errors.Count.Should().Be(1);
            var error = Runner.Errors.Single();
            error.Tag.Should().Be(ErrorType.BadVerbSelectedError);
        }
    }
}
