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
    public class StepRepositoryTests : RepositoryTestFixtureBase
    {
        private IStepRepository Sut { get; set; }
        
        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();

            Sut = new StepRepository(DbConnectionFactory);
        }

        [Test]
        public async Task It_Should_Create()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            // Act
            var id = await Sut.Create(buildStep);

            // Assert
            id.Should().BeGreaterThan(0);
            
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Should().BeEquivalentTo(buildStep);
        }

        [Test]
        public async Task It_Should_Update()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);

            var fetchedBuildStep = await Sut.Get(id);

            // Act
            fetchedBuildStep.Description = "Updated1";
            await Sut.Update(fetchedBuildStep);

            // Assert
            var updatedBuildStep = await Sut.Get(id);
            updatedBuildStep.Should().BeEquivalentTo(fetchedBuildStep);
        }
        
        [Test]
        public async Task It_Should_Delete()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);
            
            // Act
            await Sut.Delete(id);
            
            // Assert
            var fetchedBuildStep = await Sut.GetAll("123");
            fetchedBuildStep.Count.Should().Be(0);
        }
        
        [Test]
        public async Task It_Should_Get()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);

            // Act
            var fetchedBuildStep = await Sut.Get(id);

            // Assert
            fetchedBuildStep.Should().BeEquivalentTo(buildStep);
        }
        
        [Test]
        public async Task It_Should_Get_By_StepId()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            await Sut.Create(buildStep);

            // Act
            var fetchedBuildStep = await Sut.Get("123", buildStep.StepId);

            // Assert
            fetchedBuildStep.Should().BeEquivalentTo(buildStep);
        }
        
        [Test]
        public async Task It_Should_Get_All()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var testStep = Steps.Test;
            testStep.BatchId = "123";
            await Sut.Create(testStep);

            // Act
            var all = await Sut.GetAll("123");

            // Assert
            all.Count.Should().Be(2);

            var fetchedBuildStep = all.SingleOrDefault(c => c.StepId == buildStep.StepId);
            fetchedBuildStep.Should().NotBeNull();
            fetchedBuildStep.Should().BeEquivalentTo(buildStep);
            
            var fetchedTestStep = all.SingleOrDefault(c => c.StepId == testStep.StepId);
            fetchedTestStep.Should().NotBeNull();
            fetchedTestStep.Should().BeEquivalentTo(testStep);
        }
        
        [Test]
        public async Task It_Should_UpdateState()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);

            // Act
            await Sut.UpdateState(id, StepState.Stopped);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.State.Should().BeEquivalentTo(StepState.Stopped);
            fetchedBuildStep.Should().BeEquivalentTo(buildStep, o => o.Excluding(c => c.State));
        }
        
        [Test]
        public async Task It_Should_Create_StepVariable()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);

            var testFilterVariable = StepVariables.TestFilter;
            testFilterVariable.StepId = id;

            // Act
            await Sut.CreateOrUpdateStepVariable(testFilterVariable);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Variables.Count.Should().Be(3);

            var fetchedTestFilterVariable =
                fetchedBuildStep.Variables.SingleOrDefault(o => o.VariableId == testFilterVariable.VariableId);
            fetchedTestFilterVariable.Should().NotBeNull();
            fetchedTestFilterVariable.Should().BeEquivalentTo(testFilterVariable);
        }
        
        [Test]
        public async Task It_Should_Update_StepVariable()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);

            var profileVariable = buildStep.Variables.Single(o => o.VariableId == StepVariables.Profile.VariableId);
            profileVariable.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateStepVariable(profileVariable);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Variables.Count.Should().Be(2);

            var fetchedProfileVariable =
                fetchedBuildStep.Variables.SingleOrDefault(o => o.VariableId == StepVariables.Profile.VariableId);
            fetchedProfileVariable.Should().NotBeNull();
            fetchedProfileVariable.Should().BeEquivalentTo(profileVariable);
        }
        
        [Test]
        public async Task It_Should_Get_StepVariable()
        {
            // Arrange
            var profileVariable = StepVariables.Profile;
            profileVariable.StepId = 123;

            var id = await Sut.CreateOrUpdateStepVariable(profileVariable);

            // Act
            var fetchedVariable = await Sut.GetStepVariable(id);

            // Assert
            fetchedVariable.Should().BeEquivalentTo(profileVariable);
        }
        
        [Test]
        public async Task It_Should_Get_StepVariable_By_VariableId()
        {
            // Arrange
            var profileVariable = StepVariables.Profile;
            profileVariable.StepId = 123;

            await Sut.CreateOrUpdateStepVariable(profileVariable);

            // Act
            var fetchedVariable = await Sut.GetStepVariable(123, profileVariable.VariableId);

            // Assert
            fetchedVariable.Should().BeEquivalentTo(profileVariable);
        }
        
        [Test]
        public async Task It_Should_Get_All_StepVariables()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.StepId = "123";

            var id = await Sut.Create(buildStep);

            // Act
            var fetchedStepVariables = await Sut.GetAllStepVariables(id);

            // Assert
            fetchedStepVariables.Count.Should().Be(2);
            fetchedStepVariables.Should().BeEquivalentTo(buildStep.Variables);
        }
        
        [Test]
        public async Task It_Should_Delete_StepVariable()
        {
            // Arrange
            var profileVariable = StepVariables.Profile;
            profileVariable.StepId = 123;

            var id = await Sut.CreateOrUpdateStepVariable(profileVariable);

            // Act
            await Sut.DeleteStepVariable(id);

            // Assert
            var fetchedVariable = await Sut.GetStepVariable(id);
            fetchedVariable.Should().BeNull();
        }
        
        [Test]
        public async Task It_Should_Create_StepOption()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);

            var buildWhenNoErrorsOption = StepOptions.BuildWhenNoErrors;
            buildWhenNoErrorsOption.StepId = id;

            // Act
            await Sut.CreateOrUpdateStepOption(buildWhenNoErrorsOption);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Options.Count.Should().Be(2);

            var fetchedBuildWhenNoErrorsOption =
                fetchedBuildStep.Options.SingleOrDefault(o => o.OptionId == buildWhenNoErrorsOption.OptionId);
            fetchedBuildWhenNoErrorsOption.Should().NotBeNull();
            fetchedBuildWhenNoErrorsOption.Should().BeEquivalentTo(buildWhenNoErrorsOption);
        }
        
        [Test]
        public async Task It_Should_Update_StepOption()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);

            var buildAlwaysOption = buildStep.Options.Single(o => o.OptionId == StepOptions.BuildAlways.OptionId);
            buildAlwaysOption.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateStepOption(buildAlwaysOption);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Options.Count.Should().Be(1);

            var fetchedBuildAlwaysOption =
                fetchedBuildStep.Options.SingleOrDefault(o => o.OptionId == StepOptions.BuildAlways.OptionId);
            fetchedBuildAlwaysOption.Should().NotBeNull();
            fetchedBuildAlwaysOption.Should().BeEquivalentTo(buildAlwaysOption);
        }
        
        [Test]
        public async Task It_Should_Get_StepOption()
        {
            // Arrange
            var buildAlwaysOption = StepOptions.BuildAlways;
            buildAlwaysOption.StepId = 123;

            var id = await Sut.CreateOrUpdateStepOption(buildAlwaysOption);

            // Act
            var fetchedOption = await Sut.GetStepOption(id);

            // Assert
            fetchedOption.Should().BeEquivalentTo(buildAlwaysOption);
        }
        
        [Test]
        public async Task It_Should_Get_StepOption_By_OptionId()
        {
            // Arrange
            var buildAlwaysOption = StepOptions.BuildAlways;
            buildAlwaysOption.StepId = 123;

            await Sut.CreateOrUpdateStepOption(buildAlwaysOption);

            // Act
            var fetchedOption = await Sut.GetStepOption(123, buildAlwaysOption.OptionId);

            // Assert
            fetchedOption.Should().BeEquivalentTo(buildAlwaysOption);
        }
        
        [Test]
        public async Task It_Should_Get_All_StepOptions()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            var id = await Sut.Create(buildStep);

            // Act
            var fetchedStepOptions = await Sut.GetAllStepOptions(id); 

            // Assert
            fetchedStepOptions.Count.Should().Be(1);
            fetchedStepOptions.Should().BeEquivalentTo(buildStep.Options);
        }
        
        [Test]
        public async Task It_Should_Delete_StepOption()
        {
            // Arrange
            var buildAlwaysStep = StepOptions.BuildAlways;
            buildAlwaysStep.StepId = 123;

            var id = await Sut.CreateOrUpdateStepOption(buildAlwaysStep);

            // Act
            await Sut.DeleteStepOption(id);

            // Assert
            var fetchedOption = await Sut.GetStepOption(id);
            fetchedOption.Should().BeNull();
        }
        
        [Test]
        public async Task It_Should_Create_StepArtifact()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.StepId = "123";

            var id = await Sut.Create(buildStep);

            var testResultsArtifact = StepArtifacts.TestResults;
            testResultsArtifact.StepId = id;

            // Act
            await Sut.CreateStepArtifact(testResultsArtifact);

            // Assert
            var fetchedStep = await Sut.Get(id);
            fetchedStep.OutputArtifacts.Count.Should().Be(2);

            var fetchedTestResultsArtifact =
                fetchedStep.OutputArtifacts.SingleOrDefault(o => o.ArtifactId == testResultsArtifact.ArtifactId);
            fetchedTestResultsArtifact.Should().NotBeNull();
            fetchedTestResultsArtifact.Should().BeEquivalentTo(testResultsArtifact);
        }
        
        [Test]
        public async Task It_Should_Update_StepArtifact()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.StepId = "123";

            var id = await Sut.Create(buildStep);

            var sourceArtifact = buildStep.InputArtifacts.Single(o => o.ArtifactId == StepArtifacts.Source.ArtifactId);
            sourceArtifact.Description = "Updated";

            // Act
            await Sut.UpdateStepArtifact(sourceArtifact);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.InputArtifacts.Count.Should().Be(1);

            var fetchedSourceArtifact =
                fetchedBuildStep.InputArtifacts.SingleOrDefault(o => o.ArtifactId == StepArtifacts.Source.ArtifactId);
            fetchedSourceArtifact.Should().NotBeNull();
            fetchedSourceArtifact.Should().BeEquivalentTo(sourceArtifact);
        }
        
        [Test]
        public async Task It_Should_Get_StepArtifact()
        {
            // Arrange
            var binariesArtifact = StepArtifacts.Binaries;
            binariesArtifact.StepId = 123;

            var id = await Sut.CreateStepArtifact(binariesArtifact);

            // Act
            var fetchedArtifact = await Sut.GetStepArtifact(id);

            // Assert
            fetchedArtifact.Should().BeEquivalentTo(binariesArtifact);
        }
        
        [Test]
        public async Task It_Should_Get_StepArtifact_By_ArtifactId()
        {
            // Arrange
            var binariesArtifact = StepArtifacts.Binaries;
            binariesArtifact.StepId = 123;

            await Sut.CreateStepArtifact(binariesArtifact);

            // Act
            var fetchedArtifact = await Sut.GetStepArtifact(123, binariesArtifact.ArtifactId);

            // Assert
            fetchedArtifact.Should().BeEquivalentTo(binariesArtifact);
        }
        
        [Test]
        public async Task It_Should_Get_All_StepArtifacts()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.StepId = "123";

            var id = await Sut.Create(buildStep);

            // Act
            var fetchedArtifacts = await Sut.GetAllStepArtifacts(id);

            // Assert
            fetchedArtifacts.Count.Should().Be(2);
            fetchedArtifacts.Count(a => a.OutputArtifact).Should().Be(1);
        }
        
        [Test]
        public async Task It_Should_Delete_StepArtifact()
        {
            // Arrange
            var binariesArtifact = StepArtifacts.Binaries;
            binariesArtifact.StepId = 123;

            var id = await Sut.CreateStepArtifact(binariesArtifact);

            // Act
            await Sut.DeleteStepArtifact(id);

            // Assert
            var fetchedVariable = await Sut.GetStepArtifact(id);
            fetchedVariable.Should().BeNull();
        }
        
        [Test]
        public async Task It_Should_Create_StepArtifactOption()
        {
            // Arrange
            var compressOption = StepArtifactOptions.Compress;
            compressOption.StepArtifactId = 123;

            // Act
            var id = await Sut.CreateOrUpdateStepArtifactOption(compressOption);

            // Assert
            var fetchedArtifactOption = await Sut.GetStepArtifactOption(id);
            fetchedArtifactOption.Should().BeEquivalentTo(compressOption);
        }
        
        [Test]
        public async Task It_Should_Update_StepArtifactOption()
        {
            // Arrange
            var compressOption = StepArtifactOptions.Compress;
            compressOption.StepArtifactId = 123;

            var id = await Sut.CreateOrUpdateStepArtifactOption(compressOption);

            var fetchedOption = await Sut.GetStepArtifactOption(id);
            fetchedOption.Description = "Updated";
            
            // Act
            await Sut.CreateOrUpdateStepArtifactOption(fetchedOption);

            // Assert
            var updatedOption = await Sut.GetStepArtifactOption(id);
            updatedOption.Should().BeEquivalentTo(fetchedOption);
        }
        
        [Test]
        public async Task It_Should_Get_StepArtifactOption()
        {
            // Arrange
            var compressOption = StepArtifactOptions.Compress;
            compressOption.StepArtifactId = 123;

            var id = await Sut.CreateOrUpdateStepArtifactOption(compressOption);

            // Act
            var fetchedOption = await Sut.GetStepArtifactOption(id);

            // Assert
            fetchedOption.Should().BeEquivalentTo(compressOption);
        }
        
        [Test]
        public async Task It_Should_Get_StepArtifactOption_By_OptionId()
        {
            // Arrange
            var compressOption = StepArtifactOptions.Compress;
            compressOption.StepArtifactId = 123;

            await Sut.CreateOrUpdateStepArtifactOption(compressOption);

            // Act
            var fetchedOption = await Sut.GetStepArtifactOption(123, compressOption.OptionId);

            // Assert
            fetchedOption.Should().BeEquivalentTo(compressOption);
        }
        
        [Test]
        public async Task It_Should_Get_All_StepArtifactOptions()
        {
            // Arrange
            var extractOption = StepArtifactOptions.Extract;
            extractOption.StepArtifactId = 123;
            await Sut.CreateOrUpdateStepArtifactOption(extractOption);

            var compressOption = StepArtifactOptions.Compress;
            compressOption.StepArtifactId = 123;
            await Sut.CreateOrUpdateStepArtifactOption(compressOption);

            // Act
            var allOptions = await Sut.GetAllStepArtifactOptions(123);

            // Assert
            allOptions.Count.Should().Be(2);
            var fetchedExtractOption = allOptions.SingleOrDefault(o => o.OptionId == extractOption.OptionId);
            fetchedExtractOption.Should().NotBeNull();
            fetchedExtractOption.Should().BeEquivalentTo(extractOption);
            var fetchedCompressOption = allOptions.SingleOrDefault(o => o.OptionId == compressOption.OptionId);
            fetchedCompressOption.Should().NotBeNull();
            fetchedCompressOption.Should().BeEquivalentTo(compressOption);
        }
        
        [Test]
        public async Task It_Should_Delete_StepArtifactOption()
        {
            // Arrange
            var compressOption = StepArtifactOptions.Compress;
            compressOption.StepArtifactId = 123;

            var id = await Sut.CreateOrUpdateStepArtifactOption(compressOption);

            // Act
            await Sut.DeleteStepArtifactOption(id);

            // Assert
            var fetchedOption = await Sut.GetStepArtifactOption(id);
            fetchedOption.Should().BeNull();
        }
    }
}