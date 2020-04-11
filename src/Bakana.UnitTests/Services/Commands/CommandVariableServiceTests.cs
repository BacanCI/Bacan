using System.Net;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceInterface.Commands;
using Bakana.ServiceModels.Commands;
using Bakana.TestData.ServiceModels;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ServiceStack;
using Command = Bakana.Core.Entities.Command;
using CommandVariables = Bakana.TestData.Entities.CommandVariables;

namespace Bakana.UnitTests.Services.Commands
{
    public class CommandVariableServiceTests : ServiceTestFixtureBase<CommandVariableService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestStepName = "TestStep";
        private const string TestCommandName = "TestCommand";
        private const string TestCommandVariableName = "TestCommandVariable";
        
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
        public async Task It_Should_Create_CommandVariable()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(new Command
            {
                Id = 123
            });
            
            commandRepository.DoesCommandVariableExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);
            
            var request = CreateCommandVariables.Extract;
            request.BatchId = TestBatchId;
            request.StepName = TestStepName;
            request.CommandName = TestCommandName;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().CreateOrUpdateCommandVariable(Arg.Is<CommandVariable>(a =>
                a.CommandId == 123 &&
                a.Name == request.VariableName &&
                a.Description == request.Description));
        }

        [Test]
        public void Create_CommandVariable_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateCommandVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Create_CommandVariable_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new CreateCommandVariableRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Create_CommandVariable_Should_Throw_With_Invalid_Command_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new CreateCommandVariableRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }

        [Test]
        public void Create_CommandVariable_Should_Throw_With_Existing_Variable_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(new Command());

            commandRepository.DoesCommandVariableExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateCommandVariableRequest
            {
                VariableName = TestCommandVariableName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Command Variable TestCommandVariable already exists");
        }
        
        [Test]
        public async Task It_Should_Get_CommandVariable()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());
            
            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command());

            var commandVariable = CommandVariables.ConnectionString;
            commandRepository.GetCommandVariable(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(commandVariable);

            var request = new GetCommandVariableRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.DomainModels.CommandVariables.ConnectionString, 
                o => o.ExcludingMissingMembers());
            response.VariableName.Should().Be(TestData.DomainModels.CommandVariables.ConnectionString.Name);
        }
        
        [Test]
        public void Get_CommandVariable_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetCommandVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_CommandVariable_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetCommandVariableRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Get_CommandVariable_Should_Throw_With_Invalid_Command_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetCommandVariableRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
        
        [Test]
        public void Get_CommandVariable_Should_Throw_With_Invalid_Variable_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command());

            commandRepository.GetCommandVariable(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetCommandVariableRequest
            {
                VariableName = TestCommandVariableName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command Variable TestCommandVariable not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_CommandVariable()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());
            
            var command = TestData.Entities.Commands.Test;
            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(command);

            var request = new GetAllCommandVariableRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Variables.Should().BeEquivalentTo(TestData.DomainModels.Commands.Test.Variables);
        }
        
        [Test]
        public void Get_All_CommandVariables_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllCommandVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_All_CommandVariables_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllCommandVariableRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Get_All_CommandVariables_Should_Throw_With_Invalid_Command_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllCommandVariableRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
        
        [Test]
        public async Task It_Should_Update_CommandVariable()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command
                {
                    Id = 123
                });

            commandRepository.GetCommandVariable(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new CommandVariable
                {
                    Id = 456
                });
            
            var request = UpdateCommandVariables.Extract;
            request.CommandName = TestCommandName;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().CreateOrUpdateCommandVariable(Arg.Is<CommandVariable>(a =>
                a.Id == 456 && 
                a.CommandId == 123 &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_CommandVariable_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateCommandVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Update_CommandVariable_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateCommandVariableRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Update_CommandVariable_Should_Throw_With_Invalid_Command_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).ReturnsNull();

            var request = new UpdateCommandVariableRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
        
        [Test]
        public void Update_CommandVariable_Should_Throw_With_Invalid_Command_Variable_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command());

            commandRepository.GetCommandVariable(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateCommandVariableRequest
            {
                VariableName = TestCommandVariableName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command Variable TestCommandVariable not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Command_Variable()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command());

            var existingCommandVariable = new CommandVariable
            {
                Id = 123
            };
            commandRepository.GetCommandVariable(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(existingCommandVariable);

            var request = new DeleteCommandVariableRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().DeleteCommandVariable(Arg.Is<ulong>(a =>
                a == existingCommandVariable.Id));
        }
        
        [Test]
        public void Delete_Command_Variable_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteCommandVariableRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Delete_Command_Variable_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteCommandVariableRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Delete_Command_Variable_Should_Throw_With_Invalid_Command_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).ReturnsNull();

            var request = new DeleteCommandVariableRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }

        [Test]
        public void Delete_Command_Variable_Should_Throw_With_Invalid_Command_Variable_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(new Command());

            commandRepository.GetCommandVariable(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteCommandVariableRequest
            {
                VariableName = TestCommandVariableName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command Variable TestCommandVariable not found");
        }
    }
}