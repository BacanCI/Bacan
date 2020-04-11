using Bakana.ServiceInterface.Batches;
using Bakana.TestData.ServiceModels;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.IntegrationTests.Services
{
    [TestFixture]
    public class BatchServiceTests : ServiceTestFixtureBase<BatchService>
    {
        [Test]
        public void It_Should_Create_Batch()
        {
            // Arrange
            var testBatch = CreateBatches.FullyPopulated;
            
            // Act
            var response = Sut.Post(testBatch);
            
            // Assert
            response.BatchId.Should().NotBeEmpty();
        }
    }
}