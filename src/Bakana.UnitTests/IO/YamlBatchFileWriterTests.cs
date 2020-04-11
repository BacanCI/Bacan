using Bakana.Core.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.IO
{
    [TestFixture]
    public class YamlBatchFileWriterTests : BatchFileTestFixtureBase
    {
        private IBatchFileWriter Sut { get; }
        
        public YamlBatchFileWriterTests()
        {
            Sut = new YamlBatchFileWriter(FileSystem);
        }
        
        [Test]
        public void It_Should_Serialize_Batch_And_Write_To_File()
        {
            // Act
            Sut.WriteFile(TestData.DomainModels.Batches.FullyPopulated, @"c:\test.yaml");

            // Assert
            var fetchedBatch = ReadYamlFile(@"c:\test.yaml");
            fetchedBatch.Should().BeEquivalentTo(TestData.DomainModels.Batches.FullyPopulated);
        }
    }
}