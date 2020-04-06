using System;
using System.Net;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceInterface.Batches;
using Bakana.ServiceModels.Batches;
using Bakana.TestData.ServiceModels;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ServiceStack;
using Batches = Bakana.TestData.Entities.Batches;

namespace Bakana.UnitTests.Services
{
    [TestFixture]
    public class BatchServiceTests : ServiceTestFixtureBase<BatchService>
    {
        private const string TestBatchId = "TestBatch";
        
        private IBatchRepository batchRepository;
        private IShortIdGenerator shortIdGenerator;

        protected override void ConfigureAppHost(IContainer container)
        {
            batchRepository = Substitute.For<IBatchRepository>();
            container.AddTransient(() => batchRepository);

            shortIdGenerator = Substitute.For<IShortIdGenerator>();
            container.AddTransient(() => shortIdGenerator);
        }

        [Test]
        public async Task It_Should_Create_Batch()
        {
            // Arrange
            shortIdGenerator.Generate().Returns(TestBatchId);

            var request = TestData.ServiceModels.Batches.FullyPopulated;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.BatchId.Should().Be(TestBatchId);
            await batchRepository.Received().Create(Arg.Is<Batch>(a =>
                a.Id == TestBatchId &&
                a.CreatedOn != DateTime.MinValue &&
                a.CreatedOn <= DateTime.UtcNow &&
                a.Options.Count == request.Options.Count && 
                a.Variables.Count == request.Variables.Count &&
                a.Steps.Count == request.Steps.Count && 
                a.Artifacts.Count == request.Artifacts.Count));
        }

        [Test]
        public async Task It_Should_Get_Batch()
        {
            // Arrange
            var batch = Batches.FullyPopulated;
            batch.Id = TestBatchId;
            batchRepository.Get(Arg.Any<string>()).Returns(batch);
            
            var request = new GetBatchRequest
            {
                BatchId = TestBatchId
            };

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.Batches.FullyPopulated);
        }
        
        [Test]
        public void Get_Batch_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.Get(Arg.Any<string>()).ReturnsNull();

            var request = new GetBatchRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public async Task It_Should_Update_Batch()
        {
            // Arrange
            batchRepository.Update(Arg.Any<Batch>()).Returns(true);

            var request = UpdateBatches.FullyPopulated;
            request.BatchId = TestBatchId;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().Update(Arg.Is<Batch>(a =>
                a.Id == TestBatchId &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_Batch_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.Update(Arg.Any<Batch>()).Returns(false);

            var request = new UpdateBatchRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Batch()
        {
            // Arrange
            batchRepository.Delete(Arg.Any<string>()).Returns(true);
            
            var request = new DeleteBatchRequest
            {
                BatchId = TestBatchId
            };

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().Delete(Arg.Is<string>(a =>
                a == TestBatchId));
        }
        
        [Test]
        public void Delete_Batch_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.Delete(Arg.Any<string>()).Returns(false);

            var request = new DeleteBatchRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
    }
}