using Bakana.Core.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.IO
{
    [TestFixture]
    public class JsonBatchFileReaderTests : BatchFileTestFixtureBase
    {
        private IBatchFileReader Sut { get; }
        
        public JsonBatchFileReaderTests()
        {
            Sut = new JsonBatchFileReader(FileSystem);
        }
        
        [Test]
        public void It_Should_Load_And_Deserialize_Batch_From_File()
        {
            // Act
            var batch = Sut.ReadFile(FullyPopulatedJsonFile);
            
            // Assert
            batch.Should().BeEquivalentTo(TestData.DomainModels.Batches.FullyPopulated);
        }
    }
}