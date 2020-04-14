using System.Threading.Tasks;
using Bakana.Options;
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
            new object[] { "batch 123 Upload abc.zip --name def.zip", ExitCodes.Success, 
                new BatchOptions
                {
                    BatchId = "123",
                    Operation = BatchOperation.Upload,
                    FileName = "abc.zip",
                    Name = "def.zip"
                }
            },
            new object[] { "BATCH 123 UPLOAD abc.zip --NAME def.zip", ExitCodes.InvalidArguments, null },
            new object[] { "", ExitCodes.InvalidArguments, null },
        };
    }
}
