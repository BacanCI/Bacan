using System.Net;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceInterface.Commands;
using Bakana.ServiceModels.Commands;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ServiceStack;
using Command = Bakana.Core.Entities.Command;

namespace Bakana.UnitTests.Services.Commands
{
    public class CommandServiceTests : ServiceTestFixtureBase<CommandService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestStepId = "TestStep";
        private const string TestCommandId = "TestCommand";

        private IBatchRepository batchRepository;
        private IStepRepository stepRepository;
        private ICommandRepository commandRepository;

        protected override void ConfigureAppHost(IContainer container)
        {
            batchRepository = Substitute.For<IBatchRepository>();
            container.AddTransient(() => batchRepository);
            
            stepRepository = Substitute.For<IStepRepository>();
            container.AddTransient(() => stepRepository);

            commandRepository = Substitute.For<ICommandRepository>();
            container.AddTransient(() => commandRepository);
        }

        [Test]
        public async Task It_Should_Create_Command()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step
                {
                    Id = 123
                });
            
            commandRepository.DoesCommandExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);

            var request = TestData.ServiceModels.CreateCommands.Test;
            request.BatchId = TestBatchId;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().Create(Arg.Is<Command>(a =>
                a.StepId == 123 &&
                a.CommandId == request.CommandId &&
                a.Description == request.Description &&
                a.Options.Count == request.Options.Count && 
                a.Variables.Count == request.Variables.Count));
        }

        [Test]
        public void Create_Command_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateCommandRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Create_Command_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new CreateCommandRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Create_Command_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.DoesCommandExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateCommandRequest
            {
                CommandId = TestCommandId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Command TestCommand already exists");
        }
        
        [Test]
        public async Task It_Should_Get_Command()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());
            
            var command = TestData.Entities.Commands.DotNetRestore;
            command.StepId = 123;
            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(command);
            
            var request = new GetCommandRequest
            {
                BatchId = TestBatchId,
                StepId = TestStepId,
                CommandId = TestCommandId
            };

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.Commands.DotNetRestore);
        }
        
        [Test]
        public void Get_Command_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);
            
            var request = new GetCommandRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Get_Command_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            
            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new GetCommandRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Get_Command_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();
            
            var request = new GetCommandRequest
            {
                CommandId = TestCommandId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_Commands()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            
            var step = TestData.Entities.Steps.Build;
            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(step);
            
            var request = new GetAllCommandsRequest
            {
                BatchId = TestBatchId,
                StepId = TestStepId
            };

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Commands.Should().BeEquivalentTo(TestData.ServiceModels.Steps.Build.Commands);
        }
        
        [Test]
        public void Get_All_Commands_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);
            
            var request = new GetAllCommandsRequest
            {
                BatchId = TestBatchId,
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Get_All_Commands_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllCommandsRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public async Task It_Should_Update_Command()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step
                {
                    Id = 123
                });

            commandRepository.Update(Arg.Any<Command>())
                .Returns(true);

            var request = TestData.ServiceModels.UpdateCommands.Deploy;
            request.BatchId = TestBatchId;
            request.StepId = TestStepId;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().Update(Arg.Is<Command>(a =>
                a.StepId == 123 &&
                a.CommandId == request.CommandId &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_Command_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateCommandRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Update_Command_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateCommandRequest
            {
                StepId = TestStepId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Update_Command_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Update(Arg.Any<Command>())
                .Returns(false);

            var request = new UpdateCommandRequest
            {
                CommandId = TestCommandId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Command()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step
                {
                    Id = 123
                });

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(new Command
            {
                Id = 123
            });
            
            var request = new DeleteCommandRequest
            {
                BatchId = TestBatchId,
                CommandId = TestCommandId
            };

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().Delete(Arg.Is<ulong>(a =>
                a == 123));
        }
        
        [Test]
        public void Delete_Command_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteCommandRequest
            {
                BatchId = TestBatchId,
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
        
        [Test]
        public void Delete_Command_Should_Throw_With_Invalid_Step_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteCommandRequest
            {
                StepId = TestStepId,
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Delete_Command_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteCommandRequest
            {
                CommandId = TestCommandId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
    }
}