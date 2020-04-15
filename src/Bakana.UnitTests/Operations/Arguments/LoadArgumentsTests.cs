using System.Linq;
using System.Threading.Tasks;
using Bakana.Options;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Operations.Arguments
{
    [TestFixture]
    public class LoadArgumentsTests : ArgumentsTestFixtureBase<LoadOptions>
    {
        [TestCaseSource(nameof(_argumentCases))]
        public async Task Load_Options_Should_Be_Equivalent_To_CLI_Arguments(string cliArguments, int expectedExitCode, LoadOptions expectedOptions)
        {
            await Assert_Options_Should_Be_Equivalent_To_CLI_Arguments(cliArguments, expectedExitCode, expectedOptions);
        }

        private static object[] _argumentCases =
        {
            new object[] { "LOAD", ExitCodes.InvalidArguments, null },
            new object[] { "load", ExitCodes.InvalidArguments, null },
            new object[] { "load abc.zip", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip"
                }
            },
            new object[] { "load abc.zip -f JSON", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Format = BatchFileFormat.Json
                }
            },
            new object[] { "load abc.zip -f json", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Format = BatchFileFormat.Json
                }
            },
            new object[] { "load abc.zip --format yaml", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Format = BatchFileFormat.Yaml
                }
            },
            new object[] { "load abc.zip -s", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Start = true
                }
            },
            new object[] { "load abc.zip --start", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Start = true
                }
            },
            new object[] { "load abc.zip -t", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Track = true
                }
            },
            new object[] { "load abc.zip --track", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Track = true
                }
            },
            new object[] { "load abc.zip -s -i A,B,C", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Start = true,
                    IncludeFilter = "A,B,C"
                }
            },
            new object[] { "load abc.zip -s --includeFilter A,B,C", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Start = true,
                    IncludeFilter = "A,B,C"
                }
            },
            new object[] { "load abc.zip -s -e D,E,F", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Start = true,
                    ExcludeFilter = "D,E,F"
                }
            },
            new object[] { "load abc.zip -s --excludeFilter D,E,F", ExitCodes.Success, 
                new LoadOptions
                {
                    FileName = "abc.zip",
                    Start = true,
                    ExcludeFilter = "D,E,F"
                }
            },
        };

        [Test]
        public async Task Test()
        {
            // Arrange
            var args = GetArgs("LOAD");

            // Act
            var result = await Runner.Run(args);
            
            result.Should().Be(ExitCodes.InvalidArguments);

            Runner.Errors.Count.Should().Be(1);
            var error = Runner.Errors.Single();
            error.Tag.Should().Be(ErrorType.BadVerbSelectedError);
        }
    }
}
