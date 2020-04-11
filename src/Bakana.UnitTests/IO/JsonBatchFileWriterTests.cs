using Bakana.Core.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.IO
{
    [TestFixture]
    public class JsonBatchFileWriterTests : BatchFileTestFixtureBase
    {
        private IBatchFileWriter Sut { get; }
        
        public JsonBatchFileWriterTests()
        {
            Sut = new JsonBatchFileWriter(FileSystem);
        }
        
        [Test]
        public void It_Should_Serialize_Batch_And_Write_To_File()
        {
            // Act
            Sut.WriteFile(TestData.DomainModels.Batches.FullyPopulated, @"c:\test.json");

            // Assert
            var fetchedBatch = ReadJsonFile(@"c:\test.json");
            fetchedBatch.Should().BeEquivalentTo(TestData.DomainModels.Batches.FullyPopulated);
        }
    }
}