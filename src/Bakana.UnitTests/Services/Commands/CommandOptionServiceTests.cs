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
using CommandOptions = Bakana.TestData.Entities.CommandOptions;

namespace Bakana.UnitTests.Services.Commands
{
    public class CommandOptionServiceTests : ServiceTestFixtureBase<CommandOptionService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestStepName = "TestStep";
        private const string TestCommandName = "TestCommand";
        private const string TestCommandOptionName = "TestCommandOption";
        
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
        public async Task It_Should_Create_CommandOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(new Command
            {
                Id = 123
            });
            
            commandRepository.DoesCommandOptionExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);
            
            var request = CreateCommandOptions.Debug;
            request.BatchId = TestBatchId;
            request.StepName = TestStepName;
            request.CommandName = TestCommandName;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().CreateOrUpdateCommandOption(Arg.Is<CommandOption>(a =>
                a.CommandId == 123 &&
                a.Name == request.OptionName &&
                a.Description == request.Description));
        }

        [Test]
        public void Create_CommandOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateCommandOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Create_CommandOption_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new CreateCommandOptionRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Create_CommandOption_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new CreateCommandOptionRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }

        [Test]
        public void Create_CommandOption_Should_Throw_With_Existing_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(new Command());

            commandRepository.DoesCommandOptionExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateCommandOptionRequest
            {
                OptionName = TestCommandOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Command Option TestCommandOption already exists");
        }
        
        [Test]
        public async Task It_Should_Get_CommandOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());
            
            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command());

            var commandOption = CommandOptions.Debug;
            commandRepository.GetCommandOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(commandOption);

            var request = new GetCommandOptionRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.CommandOptions.Debug, 
                o => o.ExcludingMissingMembers());
            response.OptionName.Should().Be(TestData.ServiceModels.CommandOptions.Debug.Name);
        }
        
        [Test]
        public void Get_CommandOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetCommandOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_CommandOption_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetCommandOptionRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Get_CommandOption_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetCommandOptionRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
        
        [Test]
        public void Get_CommandOption_Should_Throw_With_Invalid_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command());

            commandRepository.GetCommandOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetCommandOptionRequest
            {
                OptionName = TestCommandOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command Option TestCommandOption not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_CommandOption()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());
            
            var command = TestData.Entities.Commands.Test;
            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(command);

            var request = new GetAllCommandOptionRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Options.Should().BeEquivalentTo(TestData.ServiceModels.Commands.Test.Options);
        }
        
        [Test]
        public void Get_All_CommandOptions_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllCommandOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_All_CommandOptions_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllCommandOptionRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Get_All_CommandOptions_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllCommandOptionRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
        
        [Test]
        public async Task It_Should_Update_CommandOption()
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

            commandRepository.GetCommandOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new CommandOption
                {
                    Id = 456
                });
            
            var request = UpdateCommandOptions.Debug;
            request.CommandName = TestCommandName;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().CreateOrUpdateCommandOption(Arg.Is<CommandOption>(a =>
                a.Id == 456 && 
                a.CommandId == 123 &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_CommandOption_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateCommandOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Update_CommandOption_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateCommandOptionRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Update_CommandOption_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).ReturnsNull();

            var request = new UpdateCommandOptionRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }
        
        [Test]
        public void Update_CommandOption_Should_Throw_With_Invalid_Command_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command());

            commandRepository.GetCommandOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateCommandOptionRequest
            {
                OptionName = TestCommandOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command Option TestCommandOption not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Command_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new Command());

            var existingCommandOption = new CommandOption
            {
                Id = 123
            };
            commandRepository.GetCommandOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(existingCommandOption);

            var request = new DeleteCommandOptionRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await commandRepository.Received().DeleteCommandOption(Arg.Is<ulong>(a =>
                a == existingCommandOption.Id));
        }
        
        [Test]
        public void Delete_Command_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteCommandOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Delete_Command_Option_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteCommandOptionRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Delete_Command_Option_Should_Throw_With_Invalid_Command_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).ReturnsNull();

            var request = new DeleteCommandOptionRequest
            {
                CommandName = TestCommandName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command TestCommand not found");
        }

        [Test]
        public void Delete_Command_Option_Should_Throw_With_Invalid_Command_Option_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            commandRepository.Get(Arg.Any<ulong>(), Arg.Any<string>()).Returns(new Command());

            commandRepository.GetCommandOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteCommandOptionRequest
            {
                OptionName = TestCommandOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Command Option TestCommandOption not found");
        }
    }
}