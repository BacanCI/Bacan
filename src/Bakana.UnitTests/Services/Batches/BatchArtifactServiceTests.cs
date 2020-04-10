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

namespace Bakana.UnitTests.Services.Batches
{
    public class BatchArtifactServiceTests : ServiceTestFixtureBase<BatchArtifactService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestBatchArtifactName = "TestBatchArtifact";
        private const string TestBatchArtifactOptionName = "TestBatchArtifactOption";
        
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
                ArtifactName = TestBatchArtifactName
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
            response.Should().BeEquivalentTo(TestData.ServiceModels.BatchArtifacts.Package, 
                o => o.ExcludingMissingMembers());
            response.ArtifactName.Should().Be(TestData.ServiceModels.BatchArtifacts.Package.Name);
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
                ArtifactName = TestBatchArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_BatchArtifact()
        {
            // Arrange
            var batchArtifacts = TestData.Entities.Batches.FullyPopulated.Artifacts;

            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetAllBatchArtifacts(Arg.Any<string>())
                .Returns(batchArtifacts);

            var request = new GetAllBatchArtifactRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Artifacts.Should().BeEquivalentTo(TestData.ServiceModels.Batches.FullyPopulated.Artifacts);
        }
        
        [Test]
        public void Get_All_BatchArtifacts_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllBatchArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public async Task It_Should_Update_BatchArtifact()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var batchArtifact = new BatchArtifact
            {
                Id = 123
            };

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(batchArtifact);
            
            var request = UpdateBatchArtifacts.Package;
            request.BatchId = TestBatchId;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().UpdateBatchArtifact(Arg.Is<BatchArtifact>(a =>
                a.Id == 123 &&
                a.BatchId == TestBatchId &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_BatchArtifact_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateBatchArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Update_BatchArtifact_Should_Throw_With_Invalid_Batch_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateBatchArtifactRequest
            {
                ArtifactName = TestBatchArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Batch_Artifact()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var existingBatchArtifact = new BatchArtifact
            {
                Id = 123
            };
            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(existingBatchArtifact);

            batchRepository.DeleteBatchArtifact(Arg.Any<ulong>()).Returns(true);

            var request = new DeleteBatchArtifactRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().DeleteBatchArtifact(Arg.Is<ulong>(a =>
                a == existingBatchArtifact.Id));
        }
        
        [Test]
        public void Delete_Batch_Artifact_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteBatchArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Delete_Batch_Artifact_Should_Throw_With_Invalid_Batch_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteBatchArtifactRequest
            {
                ArtifactName = TestBatchArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
        
        [Test]
        public async Task It_Should_Create_BatchArtifact_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>()).Returns(new BatchArtifact
            {
                Id = 123
            });
            
            batchRepository.DoesBatchArtifactOptionExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);

            var request = CreateBatchArtifactOptions.Compress;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().CreateOrUpdateBatchArtifactOption(Arg.Is<BatchArtifactOption>(a =>
                a.BatchArtifactId == 123 &&
                a.Name == request.OptionName &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Create_Batch_Artifact_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateBatchArtifactOptionRequest
            {
                BatchId = TestBatchId
            };

            // Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Create_Batch_Artifact_Option_Should_Throw_With_Invalid_Batch_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new CreateBatchArtifactOptionRequest
            {
                ArtifactName = TestBatchArtifactName
            };

            // Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
        
        [Test]
        public void Create_Batch_Artifact_Option_Should_Throw_With_Existing_Batch_Artifact_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new BatchArtifact());

            batchRepository.DoesBatchArtifactOptionExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateBatchArtifactOptionRequest
            {
                OptionName = TestBatchArtifactOptionName
            };

            // Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Batch Artifact Option TestBatchArtifactOption already exists");
        }
        
        [Test]
        public async Task It_Should_Get_BatchArtifact_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new BatchArtifact());
            
            var batchArtifactOption = TestData.Entities.BatchArtifactOptions.Extract;
            batchRepository.GetBatchArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(batchArtifactOption);

            // Act
            var response = await Sut.Get(new GetBatchArtifactOptionRequest());

            // Assert
            response.Should().BeEquivalentTo(BatchArtifactOptions.Extract, o => o.ExcludingMissingMembers());
            response.OptionName.Should().Be(BatchArtifactOptions.Extract.Name);
        }
        
        [Test]
        public void Get_BatchArtifact_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetBatchArtifactOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Get_BatchArtifact_Option_Should_Throw_With_Invalid_Batch_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();
            
            var request = new GetBatchArtifactOptionRequest
            {
                ArtifactName = TestBatchArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
        
        [Test]
        public void Get_BatchArtifact_Option_Should_Throw_With_Invalid_Batch_Artifact_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new BatchArtifact());

            batchRepository.GetBatchArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetBatchArtifactOptionRequest
            {
                OptionName = TestBatchArtifactOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact Option TestBatchArtifactOption not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_BatchArtifact_Options()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var batchArtifact = BatchArtifacts.Package;
            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(batchArtifact);
            
            // Act
            var response = await Sut.Get(new GetAllBatchArtifactOptionRequest());

            // Assert
            response.Options.Should().BeEquivalentTo(TestData.ServiceModels.BatchArtifacts.Package.Options);
        }
        
        [Test]
        public void Get_All_BatchArtifact_Options_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllBatchArtifactOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Get_All_BatchArtifact_Options_Should_Throw_With_Invalid_Batch_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();
            
            var request = new GetAllBatchArtifactOptionRequest
            {
                ArtifactName = TestBatchArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
        
        [Test]
        public async Task It_Should_Update_BatchArtifact_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var batchArtifact = new BatchArtifact
            {
                Id = 123
            };
            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(batchArtifact);

            var batchArtifactOption = new BatchArtifactOption
            {
                Id = 456
            };
            batchRepository.GetBatchArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(batchArtifactOption);

            var request = UpdateBatchArtifactOptions.Compress;
            request.BatchId = TestBatchId;
            request.ArtifactName = TestBatchArtifactName;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().CreateOrUpdateBatchArtifactOption(Arg.Is<BatchArtifactOption>(a =>
                a.Id == 456 &&
                a.BatchArtifactId == 123 &&
                a.Name == request.OptionName &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_BatchArtifact_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateBatchArtifactOptionRequest
            {
                BatchId = TestBatchId
            };
            
            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Update_BatchArtifact_Option_Should_Throw_With_Invalid_Batch_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateBatchArtifactOptionRequest
            {
                ArtifactName = TestBatchArtifactName
            };
            
            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
        
        [Test]
        public void Update_BatchArtifact_Option_Should_Throw_With_Invalid_Batch_Artifact_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new BatchArtifact());

            batchRepository.GetBatchArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateBatchArtifactOptionRequest
            {
                OptionName = TestBatchArtifactOptionName
            };
            
            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact Option TestBatchArtifactOption not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Batch_Artifact_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var existingBatchArtifact = new BatchArtifact
            {
                Id = 123
            };
            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(existingBatchArtifact);

            var existingBatchArtifactOption = new BatchArtifactOption
            {
                Id = 456
            };
            batchRepository.GetBatchArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(existingBatchArtifactOption);

            batchRepository.DeleteBatchArtifactOption(Arg.Any<ulong>()).Returns(true);

            var request = new DeleteBatchArtifactOptionRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().DeleteBatchArtifactOption(Arg.Is<ulong>(a =>
                a == existingBatchArtifactOption.Id));
        }
        
        [Test]
        public void Delete_Batch_Artifact_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteBatchArtifactOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Delete_Batch_Artifact_Option_Should_Throw_With_Invalid_Batch_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteBatchArtifactOptionRequest
            {
                ArtifactName = TestBatchArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact TestBatchArtifact not found");
        }
        
        [Test]
        public void Delete_Batch_Artifact_Option_Should_Throw_With_Invalid_Batch_Artifact_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchArtifact(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new BatchArtifact());

            batchRepository.GetBatchArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteBatchArtifactOptionRequest
            {
                OptionName = TestBatchArtifactOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Artifact Option TestBatchArtifactOption not found");
        }
    }
}