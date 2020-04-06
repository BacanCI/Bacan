using System.Net;
using System.Threading.Tasks;
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
using BatchArtifacts = Bakana.TestData.Entities.BatchArtifacts;

namespace Bakana.UnitTests.Services
{
    public class BatchArtifactServiceTests : ServiceTestFixtureBase<BatchArtifactService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestBatchArtifactId = "TestBatchArtifact";
        
        private IBatchRepository batchRepository;

        protected override void ConfigureAppHost(IContainer container)
        {
            batchRepository = Substitute.For<IBatchRepository>();
            container.AddTransient(() => batchRepository);
        }
        
        [Test]
        public async Task It_Should_Create_BatchArtifact()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.DoesBatchArtifactExist(Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);

            var request = CreateBatchArtifacts.Package;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().CreateBatchArtifact(Arg.Is<BatchArtifact>(a =>
                a.BatchId == request.BatchId &&
                a.Options.Count == request.Options.Count));
        }

        [Test]
        public void Create_BatchArtifact_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateBatchArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Create_BatchArtifact_Should_Throw_With_Existing_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.DoesBatchArtifactExist(Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateBatchArtifactRequest
            {
                ArtifactId = TestBatchArtifactId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact already exists");
        }
        
        [Test]
        public async Task It_Should_Get_BatchArtifact()
        {
            // Arrange
            var batchArtifact = BatchArtifacts.Package;

            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(batchArtifact);

            var request = new GetBatchArtifactRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.BatchArtifacts.Package);
        }
        
        [Test]
        public void Get_BatchArtifact_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetBatchArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Get_BatchArtifact_Should_Throw_With_Invalid_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetBatchArtifactRequest
            {
                ArtifactId = TestBatchArtifactId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
    }
}