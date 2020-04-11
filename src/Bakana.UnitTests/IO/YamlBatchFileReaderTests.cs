using Bakana.Core.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.IO
{
    [TestFixture]
    public class YamlBatchFileReaderTests : BatchFileTestFixtureBase
    {
        private IBatchFileReader Sut { get; }
        
        public YamlBatchFileReaderTests()
        {
            Sut = new YamlBatchFileReader(FileSystem);
        }
        
        [Test]
        public void It_Should_Load_And_Deserialize_Batch_From_File()
        {
            // Act
            var batch = Sut.ReadFile(FullyPopulatedYamlFile);
            
            // Assert
            batch.Should().BeEquivalentTo(TestData.DomainModels.Batches.FullyPopulated);
        }
    }
}