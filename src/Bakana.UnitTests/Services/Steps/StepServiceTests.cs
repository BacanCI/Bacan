using System.Net;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceInterface.Steps;
using Bakana.ServiceModels.Steps;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ServiceStack;

namespace Bakana.UnitTests.Services.Steps
{
    public class StepServiceTests : ServiceTestFixtureBase<StepService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestStepName = "TestStep";

        private IBatchRepository batchRepository;
        private IStepRepository stepRepository;

        protected override void ConfigureAppHost(IContainer container)
        {
            batchRepository = Substitute.For<IBatchRepository>();
            container.AddTransient(() => batchRepository);
            
            stepRepository = Substitute.For<IStepRepository>();
            container.AddTransient(() => stepRepository);
        }

        [Test]
        public async Task It_Should_Create_Step()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.DoesStepExist(Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);

            var request = TestData.ServiceModels.CreateSteps.Build;
            request.BatchId = TestBatchId;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().Create(Arg.Is<Step>(a =>
                a.BatchId == request.BatchId &&
                a.Name == request.StepName &&
                a.Description == request.Description &&
                a.Options.Count == request.Options.Count && 
                a.Variables.Count == request.Variables.Count &&
                a.Commands.Count == request.Commands.Count && 
                a.Artifacts.Count == request.Artifacts.Count));
        }

        [Test]
        public void Create_Step_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateStepRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Create_Step_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.DoesStepExist(Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateStepRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Step TestStep already exists");
        }
        
        [Test]
        public async Task It_Should_Get_Step()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            
            var step = TestData.Entities.Steps.Build;
            step.BatchId = TestStepName;
            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(step);
            
            var request = new GetStepRequest
            {
                BatchId = TestBatchId,
                StepName = TestStepName
            };

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.Steps.Build, 
                o => o.ExcludingMissingMembers());
            response.StepName.Should().Be(TestData.ServiceModels.Steps.Build.Name);
        }
        
        [Test]
        public void Get_Step_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);
            
            var request = new GetStepRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Get_Step_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            
            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();
            
            var request = new GetStepRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_Steps()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            
            var steps = TestData.Entities.Batches.FullyPopulated.Steps;
            stepRepository.GetAll(Arg.Any<string>()).Returns(steps);
            
            var request = new GetAllStepsRequest
            {
                BatchId = TestBatchId,
            };

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Steps.Should().BeEquivalentTo(TestData.ServiceModels.Batches.FullyPopulated.Steps);
        }
        
        [Test]
        public void Get_All_Steps_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);
            
            var request = new GetAllStepsRequest
            {
                BatchId = TestBatchId,
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public async Task It_Should_Update_Step()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Update(Arg.Any<Step>())
                .Returns(true);

            var request = TestData.ServiceModels.UpdateSteps.Build;
            request.BatchId = TestBatchId;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().Update(Arg.Is<Step>(a =>
                a.BatchId == request.BatchId &&
                a.Name == request.StepName &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_Step_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateStepRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Update_Step_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Update(Arg.Any<Step>())
                .Returns(false);

            var request = new UpdateStepRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Step()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step
            {
                Id = 123
            });
            
            var request = new DeleteStepRequest
            {
                BatchId = TestBatchId,
                StepName = TestStepName
            };

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().Delete(Arg.Is<ulong>(a =>
                a == 123));
        }
        
        [Test]
        public void Delete_Step_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteStepRequest
            {
                BatchId = TestBatchId,
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Delete_Step_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteStepRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
    }
}