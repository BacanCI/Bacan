using Bakana.Core.Entities;
using Bakana.ServiceInterface;
using Bakana.ServiceModels;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;

namespace Bakana.UnitTests
{
    [TestFixture]
    public class MappingTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Mappers.Register();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void It_Should_Map_CreateBatchRequest_To_Batch()
        {
            // Arrange
            var fullyPopulatedRequest = TestData.ServiceModels.Batches.FullyPopulated;
            
            // Act
            var mappedBatch = fullyPopulatedRequest.ConvertTo<Batch>();
            
            // Assert
            mappedBatch.Should().BeEquivalentTo(fullyPopulatedRequest);
        }
        
        [Test]
        public void It_Should_Map_Batch_To_GetBatchResponse()
        {
            // Arrange
            var fullyPopulatedBatch = TestData.Entities.Batches.FullyPopulated;
            
            // Act
            var mappedBatch = fullyPopulatedBatch.ConvertTo<GetBatchResponse>();
            
            // Assert
            mappedBatch.Should().BeEquivalentTo(fullyPopulatedBatch, o => o.ExcludingMissingMembers());
        }
    }
}