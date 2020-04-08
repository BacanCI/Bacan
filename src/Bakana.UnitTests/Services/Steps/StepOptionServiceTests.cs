using System.Net;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceInterface.Steps;
using Bakana.ServiceModels.Steps;
using Bakana.TestData.ServiceModels;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ServiceStack;
using StepOptions = Bakana.TestData.Entities.StepOptions;

namespace Bakana.UnitTests.Services.Steps
{
    public class StepOptionServiceTests : ServiceTestFixtureBase<StepOptionService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestStepId = "TestStep";
        private const string TestStepOptionId = "TestStepOption";
        
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
        public async Task It_Should_Create_StepOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step
            {
                Id = 123
            });
            
            stepRepository.DoesStepOptionExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);
            
            var request = CreateStepOptions.BuildAlways;
            request.BatchId = TestBatchId;
            request.StepId = TestStepId;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().CreateOrUpdateStepOption(Arg.Is<StepOption>(a =>
                a.StepId == 123 &&
                a.OptionId == request.OptionId &&
                a.Description == request.Description));
        }

        [Test]
        public void Create_StepOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateStepOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Create_StepOption_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new CreateStepOptionRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Create_StepOption_Should_Throw_With_Existing_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.DoesStepOptionExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateStepOptionRequest
            {
                OptionId = TestStepOptionId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Step Option TestStepOption already exists");
        }
        
        [Test]
        public async Task It_Should_Get_StepOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step
            {
                Id = 123
            });
            
            var stepOption = StepOptions.BuildWhenNoErrors;
            stepRepository.GetStepOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(stepOption);

            var request = new GetStepOptionRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.StepOptions.BuildWhenNoErrors);
        }
        
        [Test]
        public void Get_StepOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetStepOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_StepOption_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new GetStepOptionRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Get_StepOption_Should_Throw_With_Invalid_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.GetStepOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetStepOptionRequest
            {
                OptionId = TestStepOptionId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Option TestStepOption not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_StepOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            var step = TestData.Entities.Steps.Build;
            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(step);

            var request = new GetAllStepOptionRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Options.Should().BeEquivalentTo(TestData.ServiceModels.Steps.Build.Options);
        }
        
        [Test]
        public void Get_All_StepOptions_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllStepOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_All_StepOptions_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllStepOptionRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public async Task It_Should_Update_StepOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step
                {
                    Id = 123
                });

            stepRepository.GetStepOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepOption
                {
                    Id = 456
                });
            
            var request = UpdateStepOptions.BuildAlways;
            request.StepId = TestStepId;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().CreateOrUpdateStepOption(Arg.Is<StepOption>(a =>
                a.Id == 456 && 
                a.StepId == 123 &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_StepOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateStepOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Update_StepOption_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new UpdateStepOptionRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Update_StepOption_Should_Throw_With_Invalid_Step_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.GetStepOption(Arg.Any<ulong>(), Arg.Any<string>()).ReturnsNull();

            var request = new UpdateStepOptionRequest
            {
                OptionId = TestStepOptionId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Option TestStepOption not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Step_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            var existingStepOption = new StepOption
            {
                Id = 123
            };
            stepRepository.GetStepOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(existingStepOption);

            var request = new DeleteStepOptionRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().DeleteStepOption(Arg.Is<ulong>(a =>
                a == existingStepOption.Id));
        }
        
        [Test]
        public void Delete_Step_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteStepOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Delete_Step_Option_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new DeleteStepOptionRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Delete_Step_Option_Should_Throw_With_Invalid_Step_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.GetStepOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteStepOptionRequest
            {
                OptionId = TestStepOptionId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Option TestStepOption not found");
        }
    }
}