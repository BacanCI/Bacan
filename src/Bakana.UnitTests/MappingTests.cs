using Bakana.Core.Entities;
using Bakana.ServiceInterface;
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
            var fullyPopulatedRequest = TestData.Batches.FullyPopulated;
            
            // Act
            var mappedBatch = fullyPopulatedRequest.ConvertTo<Batch>();
            
            // Assert
            mappedBatch.Should().BeEquivalentTo(fullyPopulatedRequest);
        }
        
    }
}