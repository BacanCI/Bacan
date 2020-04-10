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
using BatchOptions = Bakana.TestData.Entities.BatchOptions;

namespace Bakana.UnitTests.Services.Batches
{
    public class BatchOptionServiceTests : ServiceTestFixtureBase<BatchOptionService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestBatchOptionName = "TestBatchOption";
        
        private IBatchRepository batchRepository;

        protected override void ConfigureAppHost(IContainer container)
        {
            batchRepository = Substitute.For<IBatchRepository>();
            container.AddTransient(() => batchRepository);
        }
        
        [Test]
        public async Task It_Should_Create_BatchOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.DoesBatchOptionExist(Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);

            var request = CreateBatchOptions.Debug;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().CreateOrUpdateBatchOption(Arg.Is<BatchOption>(a =>
                a.BatchId == request.BatchId &&
                a.Name == request.OptionName &&
                a.Description == request.Description));
        }

        [Test]
        public void Create_BatchOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateBatchOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Create_BatchOption_Should_Throw_With_Existing_Option_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.DoesBatchOptionExist(Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateBatchOptionRequest
            {
                OptionName = TestBatchOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Batch Option TestBatchOption already exists");
        }
        
        [Test]
        public async Task It_Should_Get_BatchOption()
        {
            // Arrange
            var batchOption = BatchOptions.Log;

            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetBatchOption(Arg.Any<string>(), Arg.Any<string>())
                .Returns(batchOption);

            var request = new GetBatchOptionRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.BatchOptions.Log, 
                o => o.ExcludingMissingMembers());
            response.OptionName.Should().Be(TestData.ServiceModels.BatchOptions.Log.Name);
        }
        
        [Test]
        public void Get_BatchOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetBatchOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Get_BatchOption_Should_Throw_With_Invalid_Option_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetBatchOption(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetBatchOptionRequest
            {
                OptionName = TestBatchOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Option TestBatchOption not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_BatchOption()
        {
            // Arrange
            var batchOptions = TestData.Entities.Batches.FullyPopulated.Options;

            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetAllBatchOptions(Arg.Any<string>())
                .Returns(batchOptions);

            var request = new GetAllBatchOptionRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Options.Should().BeEquivalentTo(TestData.ServiceModels.Batches.FullyPopulated.Options);
        }
        
        [Test]
        public void Get_All_BatchOptions_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllBatchOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public async Task It_Should_Update_BatchOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var batchOption = new BatchOption
            {
                Id = 123
            };

            batchRepository.GetBatchOption(Arg.Any<string>(), Arg.Any<string>())
                .Returns(batchOption);
            
            var request = UpdateBatchOptions.Debug;
            request.BatchId = TestBatchId;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().CreateOrUpdateBatchOption(Arg.Is<BatchOption>(a =>
                a.Id == 123 &&
                a.BatchId == TestBatchId &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_BatchOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateBatchOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Update_BatchOption_Should_Throw_With_Invalid_Batch_Option_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchOption(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateBatchOptionRequest
            {
                OptionName = TestBatchOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Option TestBatchOption not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Batch_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var existingBatchOption = new BatchOption
            {
                Id = 123
            };
            batchRepository.GetBatchOption(Arg.Any<string>(), Arg.Any<string>())
                .Returns(existingBatchOption);

            batchRepository.DeleteBatchOption(Arg.Any<ulong>()).Returns(true);

            var request = new DeleteBatchOptionRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().DeleteBatchOption(Arg.Is<ulong>(a =>
                a == existingBatchOption.Id));
        }
        
        [Test]
        public void Delete_Batch_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteBatchOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Delete_Batch_Option_Should_Throw_With_Invalid_Batch_Option_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchOption(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteBatchOptionRequest
            {
                OptionName = TestBatchOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Option TestBatchOption not found");
        }
    }
}