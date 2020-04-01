using System.Linq;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Repositories;
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
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

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
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

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
        public async Task It_Should_Update_By_CommandId()
        {
            // Arrange
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);

            var fetchedRestoreCommand = await Sut.Get(id);

            // Act
            fetchedRestoreCommand.Description = "Updated1";
            await Sut.UpdateByCommandId(fetchedRestoreCommand);

            // Assert
            var updatedRestoreCommand = await Sut.Get(id);
            updatedRestoreCommand.Should().BeEquivalentTo(fetchedRestoreCommand);
        }

        [Test]
        public async Task It_Should_Delete()
        {
            // Arrange
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);
            
            // Act
            await Sut.Delete(id);
            
            // Assert
            var all = await Sut.GetAll(1);
            all.Count.Should().Be(0);
        }

        [Test]
        public async Task It_Should_Get()
        {
            // Arrange
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);

            // Act
            var fetchedRestoreCommand = await Sut.Get(id);

            // Assert
            fetchedRestoreCommand.Should().BeEquivalentTo(restoreCommand);
        }

        [Test]
        public async Task It_Should_GetAll()
        {
            // Arrange
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;
            var buildCommand = TestData.Commands.DotNetBuild;
            buildCommand.StepId = 1;

            await Sut.Create(restoreCommand);
            await Sut.Create(buildCommand);

            // Act
            var all = await Sut.GetAll(1);

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
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

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
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);

            var productionOption = TestData.CommandOptions.Production;
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
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);

            var option1 = restoreCommand.Options.Single(o => o.OptionId == TestData.CommandOptions.Optional1.OptionId);
            option1.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateCommandOption(option1);

            // Assert
            var fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Options.Count.Should().Be(2);

            var fetchedOption1 =
                fetchedRestoreCommand.Options.SingleOrDefault(o => o.OptionId == TestData.CommandOptions.Optional1.OptionId);
            fetchedOption1.Should().NotBeNull();
            fetchedOption1.Should().BeEquivalentTo(option1);
        }
        
        [Test]
        public async Task It_Should_Delete_CommandOption()
        {
            // Arrange
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);

            var fetchedRestoreCommand = await Sut.Get(id);
            var option1 =
                fetchedRestoreCommand.Options.Single(v => v.OptionId == TestData.CommandOptions.Optional1.OptionId);

            // Act
            await Sut.DeleteCommandOption(option1.Id);

            // Assert
            fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Options.Count.Should().Be(1);
        }

        [Test]
        public async Task It_Should_Create_CommandVariable()
        {
            // Arrange
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);

            var connectionStringVariable = TestData.CommandVariables.ConnectionString;
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
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);

            var demoVariable = restoreCommand.Variables.Single(o => o.VariableId == TestData.CommandVariables.DemoArg.VariableId);
            demoVariable.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateCommandVariable(demoVariable);

            // Assert
            var fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Variables.Count.Should().Be(2);

            var fetchedDemoVariable =
                fetchedRestoreCommand.Variables.SingleOrDefault(o => o.VariableId == TestData.CommandVariables.DemoArg.VariableId);
            fetchedDemoVariable.Should().NotBeNull();
            fetchedDemoVariable.Should().BeEquivalentTo(demoVariable);
        }
        
        [Test]
        public async Task It_Should_Delete_CommandVariable()
        {
            // Arrange
            var restoreCommand = TestData.Commands.DotNetRestore;
            restoreCommand.StepId = 1;

            var id = await Sut.Create(restoreCommand);

            var fetchedRestoreCommand = await Sut.Get(id);
            var demoVariable =
                fetchedRestoreCommand.Variables.Single(v => v.VariableId == TestData.CommandVariables.DemoArg.VariableId);

            // Act
            await Sut.DeleteCommandVariable(demoVariable.Id);

            // Assert
            fetchedRestoreCommand = await Sut.Get(id);
            fetchedRestoreCommand.Variables.Count.Should().Be(1);
        }
    }
}