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
using BatchVariables = Bakana.TestData.Entities.BatchVariables;

namespace Bakana.UnitTests.Services.Batches
{
    public class BatchVariableServiceTests : ServiceTestFixtureBase<BatchVariableService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestBatchVariableId = "TestBatchVariable";
        
        private IBatchRepository batchRepository;

        protected override void ConfigureAppHost(IContainer container)
        {
            batchRepository = Substitute.For<IBatchRepository>();
            container.AddTransient(() => batchRepository);
        }
        
        [Test]
        public async Task It_Should_Create_BatchVariable()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.DoesBatchVariableExist(Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);

            var request = CreateBatchVariables.Environment;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().CreateOrUpdateBatchVariable(Arg.Is<BatchVariable>(a =>
                a.BatchId == request.BatchId &&
                a.VariableId == request.VariableId &&
                a.Description == request.Description));
        }

        [Test]
        public void Create_BatchVariable_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateBatchVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Create_BatchVariable_Should_Throw_With_Existing_Variable_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.DoesBatchVariableExist(Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateBatchVariableRequest
            {
                VariableId = TestBatchVariableId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Batch Variable TestBatchVariable already exists");
        }
        
        [Test]
        public async Task It_Should_Get_BatchVariable()
        {
            // Arrange
            var batchVariable = BatchVariables.Schedule;

            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetBatchVariable(Arg.Any<string>(), Arg.Any<string>())
                .Returns(batchVariable);

            var request = new GetBatchVariableRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.BatchVariables.Schedule);
        }
        
        [Test]
        public void Get_BatchVariable_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetBatchVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Get_BatchVariable_Should_Throw_With_Invalid_Variable_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetBatchVariable(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetBatchVariableRequest
            {
                VariableId = TestBatchVariableId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Variable TestBatchVariable not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_BatchVariable()
        {
            // Arrange
            var batchVariables = TestData.Entities.Batches.FullyPopulated.Variables;

            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            batchRepository.GetAllBatchVariables(Arg.Any<string>())
                .Returns(batchVariables);

            var request = new GetAllBatchVariableRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Variables.Should().BeEquivalentTo(TestData.ServiceModels.Batches.FullyPopulated.Variables);
        }
        
        [Test]
        public void Get_All_BatchVariables_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllBatchVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public async Task It_Should_Update_BatchVariable()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var batchVariable = new BatchVariable
            {
                Id = 123
            };

            batchRepository.GetBatchVariable(Arg.Any<string>(), Arg.Any<string>())
                .Returns(batchVariable);
            
            var request = UpdateBatchVariables.Environment;
            request.BatchId = TestBatchId;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().CreateOrUpdateBatchVariable(Arg.Is<BatchVariable>(a =>
                a.Id == 123 &&
                a.BatchId == TestBatchId &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_BatchVariable_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateBatchVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Update_BatchVariable_Should_Throw_With_Invalid_Batch_Variable_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchVariable(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateBatchVariableRequest
            {
                VariableId = TestBatchVariableId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Variable TestBatchVariable not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Batch_Variable()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var existingBatchVariable = new BatchVariable
            {
                Id = 123
            };
            batchRepository.GetBatchVariable(Arg.Any<string>(), Arg.Any<string>())
                .Returns(existingBatchVariable);

            batchRepository.DeleteBatchVariable(Arg.Any<ulong>()).Returns(true);

            var request = new DeleteBatchVariableRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await batchRepository.Received().DeleteBatchVariable(Arg.Is<ulong>(a =>
                a == existingBatchVariable.Id));
        }
        
        [Test]
        public void Delete_Batch_Variable_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteBatchVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Delete_Batch_Variable_Should_Throw_With_Invalid_Batch_Variable_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            batchRepository.GetBatchVariable(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteBatchVariableRequest
            {
                VariableId = TestBatchVariableId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch Variable TestBatchVariable not found");
        }
    }
}