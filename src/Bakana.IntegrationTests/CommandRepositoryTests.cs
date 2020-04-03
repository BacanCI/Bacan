using System.Linq;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Repositories;
using Bakana.TestData.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.IntegrationTests
{
    [TestFixture]
    public class CommandRepositoryTests : RepositoryTestFixtureBase
    {
        private ICommandRepository Sut { get; set; }
        
        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();

            Sut = new CommandRepository(DbConnectionFactory);
        }

        [Test]
        public async Task It_Should_Create()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            // Act
            var id = await Sut.Create(restoreCommand);

            // Assert
            id.Should().BeGreaterThan(0);
            
            var fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Should().BeEquivalentTo(restoreCommand);
        }

        [Test]
        public async Task It_Should_Update()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            var fetchedRestoreCommand = await Sut.Get(id);

            // Act
            fetchedRestoreCommand.Description = "Updated1";
            await Sut.Update(fetchedRestoreCommand);

            // Assert
            var updatedRestoreCommand = await Sut.Get(id);
            updatedRestoreCommand.Should().BeEquivalentTo(fetchedRestoreCommand);
        }

        [Test]
        public async Task It_Should_Delete()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);
            
            // Act
            await Sut.Delete(id);
            
            // Assert
            var all = await Sut.GetAll(123);
            all.Count.Should().Be(0);
        }

        [Test]
        public async Task It_Should_Get()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            // Act
            var fetchedRestoreCommand = await Sut.Get(id);

            // Assert
            fetchedRestoreCommand.Should().BeEquivalentTo(restoreCommand);
        }

        [Test]
        public async Task It_Should_Get_By_CommandId()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            await Sut.Create(restoreCommand);

            // Act
            var fetchedRestoreCommand = await Sut.Get(123, restoreCommand.CommandId);

            // Assert
            fetchedRestoreCommand.Should().BeEquivalentTo(restoreCommand);
        }

        [Test]
        public async Task It_Should_GetAll()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;
            var buildCommand = Commands.DotNetBuild;
            buildCommand.StepId = 123;

            await Sut.Create(restoreCommand);
            await Sut.Create(buildCommand);

            // Act
            var all = await Sut.GetAll(123);

            // Assert
            all.Count.Should().Be(2);

            var fetchedRestoreCmd = all.SingleOrDefault(c => c.CommandId == restoreCommand.CommandId);
            fetchedRestoreCmd.Should().NotBeNull();
            fetchedRestoreCmd.Should().BeEquivalentTo(restoreCommand);
            
            var fetchedBuildCommand = all.SingleOrDefault(c => c.CommandId == buildCommand.CommandId);
            fetchedBuildCommand.Should().NotBeNull();
            fetchedBuildCommand.Should().BeEquivalentTo(buildCommand);
        }
        
        [Test]
        public async Task It_Should_Update_State()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            // Act
            await Sut.UpdateState(id, CommandState.Stopped);

            // Assert
            var fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.State.Should().BeEquivalentTo(CommandState.Stopped);
            fetchedRestoreCommand.Should().BeEquivalentTo(restoreCommand, o => o.Excluding(c => c.State));
        }
        
        [Test]
        public async Task It_Should_Create_CommandOption()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            var productionOption = CommandOptions.Production;
            productionOption.CommandId = id;

            // Act
            await Sut.CreateOrUpdateCommandOption(productionOption);

            // Assert
            var fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Options.Count.Should().Be(3);

            var fetchedProductionOption =
                fetchedRestoreCommand.Options.SingleOrDefault(o => o.OptionId == productionOption.OptionId);
            fetchedProductionOption.Should().NotBeNull();
            fetchedProductionOption.Should().BeEquivalentTo(productionOption);
        }
        
        [Test]
        public async Task It_Should_Update_CommandOption()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            var option1 = restoreCommand.Options.Single(o => o.OptionId == CommandOptions.Optional1.OptionId);
            option1.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateCommandOption(option1);

            // Assert
            var fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Options.Count.Should().Be(2);

            var fetchedOption1 =
                fetchedRestoreCommand.Options.SingleOrDefault(o => o.OptionId == CommandOptions.Optional1.OptionId);
            fetchedOption1.Should().NotBeNull();
            fetchedOption1.Should().BeEquivalentTo(option1);
        }
        
        [Test]
        public async Task It_Should_Get_CommandOption()
        {
            // Arrange
            var option1 = CommandOptions.Optional1;
            option1.CommandId = 123;

            var id = await Sut.CreateOrUpdateCommandOption(option1);

            // Act
            var fetchedOption = await Sut.GetCommandOption(id);

            // Assert
            fetchedOption.Should().BeEquivalentTo(option1);
        }

        [Test]
        public async Task It_Should_Get_CommandOption_By_CommandId()
        {
            // Arrange
            var option1 = CommandOptions.Optional1;
            option1.CommandId = 123;

            await Sut.CreateOrUpdateCommandOption(option1);

            // Act
            var fetchedOption = await Sut.GetCommandOption(123, option1.OptionId);

            // Assert
            fetchedOption.Should().BeEquivalentTo(option1);
        }

        [Test]
        public async Task It_Should_Get_All_CommandOptions()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            // Act
            var fetchedCommandOptions = await Sut.GetAllCommandOptions(id);

            // Assert
            fetchedCommandOptions.Count.Should().Be(2);
            fetchedCommandOptions.Should().BeEquivalentTo(restoreCommand.Options);
        }

        [Test]
        public async Task It_Should_Delete_CommandOption()
        {
            // Arrange
            var option1 = CommandOptions.Optional1;
            option1.CommandId = 123;

            var id = await Sut.CreateOrUpdateCommandOption(option1);

            // Act
            await Sut.DeleteCommandOption(id);

            // Assert
            var fetchedOption = await Sut.GetCommandOption(id);
            fetchedOption.Should().BeNull();
        }

        [Test]
        public async Task It_Should_Create_CommandVariable()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            var connectionStringVariable = CommandVariables.ConnectionString;
            connectionStringVariable.CommandId = id;

            // Act
            await Sut.CreateOrUpdateCommandVariable(connectionStringVariable);

            // Assert
            var fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Variables.Count.Should().Be(3);

            var fetchedConnectionStringVariable =
                fetchedRestoreCommand.Variables.SingleOrDefault(o => o.VariableId == connectionStringVariable.VariableId);
            fetchedConnectionStringVariable.Should().NotBeNull();
            fetchedConnectionStringVariable.Should().BeEquivalentTo(connectionStringVariable);
        }
        
        [Test]
        public async Task It_Should_Update_CommandVariable()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            var demoVariable = restoreCommand.Variables.Single(o => o.VariableId == CommandVariables.DemoArg.VariableId);
            demoVariable.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateCommandVariable(demoVariable);

            // Assert
            var fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Variables.Count.Should().Be(2);

            var fetchedDemoVariable =
                fetchedRestoreCommand.Variables.SingleOrDefault(o => o.VariableId == CommandVariables.DemoArg.VariableId);
            fetchedDemoVariable.Should().NotBeNull();
            fetchedDemoVariable.Should().BeEquivalentTo(demoVariable);
        }
        
        [Test]
        public async Task It_Should_Get_CommandVariable()
        {
            // Arrange
            var connectionStringVariable = CommandVariables.ConnectionString;
            connectionStringVariable.CommandId = 123;

            var id = await Sut.CreateOrUpdateCommandVariable(connectionStringVariable);

            // Act
            var fetchedVariable = await Sut.GetCommandVariable(id);

            // Assert
            fetchedVariable.Should().BeEquivalentTo(connectionStringVariable);
        }

        [Test]
        public async Task It_Should_Get_CommandVariable_By_CommandId()
        {
            // Arrange
            var connectionStringVariable = CommandVariables.ConnectionString;
            connectionStringVariable.CommandId = 123;

            await Sut.CreateOrUpdateCommandVariable(connectionStringVariable);

            // Act
            var fetchedVariable = await Sut.GetCommandVariable(123, connectionStringVariable.VariableId);

            // Assert
            fetchedVariable.Should().BeEquivalentTo(connectionStringVariable);
        }

        [Test]
        public async Task It_Should_Get_All_CommandVariables()
        {
            // Arrange
            var restoreCommand = Commands.DotNetRestore;
            restoreCommand.StepId = 123;

            var id = await Sut.Create(restoreCommand);

            // Act
            var fetchedCommandVariables = await Sut.GetAllCommandVariables(id);

            // Assert
            fetchedCommandVariables.Count.Should().Be(2);
            fetchedCommandVariables.Should().BeEquivalentTo(restoreCommand.Variables);
        }

        [Test]
        public async Task It_Should_Delete_CommandVariable()
        {
            // Arrange
            var connectionStringVariable = CommandVariables.ConnectionString;
            connectionStringVariable.CommandId = 123;

            var id = await Sut.CreateOrUpdateCommandVariable(connectionStringVariable);

            // Act
            await Sut.DeleteCommandVariable(id);

            // Assert
            var fetchedVariable = await Sut.GetCommandVariable(id);
            fetchedVariable.Should().BeNull();
        }
    }
}