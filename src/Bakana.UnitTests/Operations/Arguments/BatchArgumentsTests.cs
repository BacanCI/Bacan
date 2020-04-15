using System.Linq;
using System.Threading.Tasks;
using Bakana.Options;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Operations.Arguments
{
    [TestFixture]
    public class BatchArgumentsTests : ArgumentsTestFixtureBase<BatchOptions>
    {
        [TestCaseSource(nameof(_argumentCases))]
        public async Task Batch_Options_Should_Be_Equivalent_To_CLI_Arguments(string cliArguments, int expectedExitCode, BatchOptions expectedOptions)
        {
            await Assert_Options_Should_Be_Equivalent_To_CLI_Arguments(cliArguments, expectedExitCode, expectedOptions);
        }

        private static object[] _argumentCases =
        {
            new object[] { "BATCH", ExitCodes.InvalidArguments, null },
            new object[] { "batch", ExitCodes.InvalidArguments, null },
            new object[] { "batch 123", ExitCodes.InvalidArguments, null },
            new object[] { "batch 123 UPLOAD", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Upload,
                }
            },
            new object[] { "batch 123 Upload abc.zip -n def.zip", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Upload,
                    FileName = "abc.zip",
                    Name = "def.zip"
                }
            },
            new object[] { "batch 123 Upload abc.zip --name def.zip", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Upload,
                    FileName = "abc.zip",
                    Name = "def.zip"
                }
            },
            new object[] { "batch 123 DOWNLOAD", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Download,
                }
            },
            new object[] { @"batch 123 download def.zip -p c:\temp\ghi.zip", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Download,
                    FileName = "def.zip",
                    Path = @"c:\temp\ghi.zip"
                }
            },
            new object[] { @"batch 123 download def.zip --path c:\temp\ghi.zip", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Download,
                    FileName = "def.zip",
                    Path = @"c:\temp\ghi.zip"
                }
            },
            new object[] { "batch 123 start -i A,B,C", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Start,
                    IncludeFilter = "A,B,C"
                }
            },
            new object[] { "batch 123 start --includeFilter A,B,C", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Start,
                    IncludeFilter = "A,B,C"
                }
            },
            new object[] { "batch 123 start -e D,E,F", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Start,
                    ExcludeFilter = "D,E,F"
                }
            },
            new object[] { "batch 123 start --excludeFilter D,E,F", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Start,
                    ExcludeFilter = "D,E,F"
                }
            },
            new object[] { @"batch 123 cancel", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Cancel
                }
            },
            new object[] { @"batch 123 info", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Info
                }
            },
            new object[] { @"batch 123 start", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Start
                }
            },
            new object[] { @"batch 123 stop", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Stop
                }
            },
            new object[] { @"batch 123 track", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Track
                }
            },
        };

        [Test]
        public async Task Test()
        {
            // Arrange
            var args = GetArgs("BATCH");

            // Act
            var result = await Runner.Run(args);
            
            result.Should().Be(ExitCodes.InvalidArguments);

            Runner.Errors.Count.Should().Be(1);
            var error = Runner.Errors.Single();
            error.Tag.Should().Be(ErrorType.BadVerbSelectedError);
        }
    }
}
