using System.Linq;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Repositories;
using Bakana.TestData.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.IntegrationTests.Repositories
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
        public async Task It_Should_Create_Without_Step_Variables()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            buildStep.Variables = null;

            // Act
            var id = await Sut.Create(buildStep);

            // Assert
            id.Should().BeGreaterThan(0);

            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Variables.Should().BeEmpty();
            fetchedBuildStep.Should().BeEquivalentTo(buildStep, options => options.Excluding(p => p.Variables));
        }

        [Test]
        public async Task It_Should_Create_Without_Step_Options()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            buildStep.Options = null;

            // Act
            var id = await Sut.Create(buildStep);

            // Assert
            id.Should().BeGreaterThan(0);

            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Options.Should().BeEmpty();
            fetchedBuildStep.Should().BeEquivalentTo(buildStep, options => options.Excluding(p => p.Options));
        }

        [Test]
        public async Task It_Should_Create_Without_Step_Artifacts()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            buildStep.Artifacts = null;

            // Act
            var id = await Sut.Create(buildStep);

            // Assert
            id.Should().BeGreaterThan(0);

            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Artifacts.Should().BeEmpty();
            fetchedBuildStep.Should().BeEquivalentTo(buildStep, options => options.Excluding(p => p.Artifacts));
        }

        [Test]
        public async Task It_Should_Create_Without_Commands()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            buildStep.Commands = null;

            // Act
            var id = await Sut.Create(buildStep);

            // Assert
            id.Should().BeGreaterThan(0);

            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Commands.Should().BeEmpty();
            fetchedBuildStep.Should().BeEquivalentTo(buildStep, options => options.Excluding(p => p.Commands));
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
        public async Task It_Should_Get_By_StepName()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";

            await Sut.Create(buildStep);

            // Act
            var fetchedBuildStep = await Sut.Get("123", buildStep.Name);

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

            var fetchedBuildStep = all.SingleOrDefault(c => c.Name == buildStep.Name);
            fetchedBuildStep.Should().NotBeNull();
            fetchedBuildStep.Should().BeEquivalentTo(buildStep);
            
            var fetchedTestStep = all.SingleOrDefault(c => c.Name == testStep.Name);
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
        public async Task It_Should_Return_Step_Exist()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            // Act
            var doesStepExist = await Sut.DoesStepExist(buildStep.BatchId, buildStep.Name);

            // Assert
            doesStepExist.Should().BeTrue();
        }

        [Test]
        public async Task It_Should_Return_Step_Does_Not_Exist_When_BatchId_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            // Act
            var doesStepExist = await Sut.DoesStepExist("BatchId_1", buildStep.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Does_Not_Exist_When_StepName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            // Act
            var doesStepExist = await Sut.DoesStepExist(buildStep.BatchId, "StepName_1");

            // Assert
            doesStepExist.Should().BeFalse();
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
                fetchedBuildStep.Variables.SingleOrDefault(o => o.Name == testFilterVariable.Name);
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

            var profileVariable = buildStep.Variables.Single(o => o.Name == StepVariables.Profile.Name);
            profileVariable.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateStepVariable(profileVariable);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Variables.Count.Should().Be(2);

            var fetchedProfileVariable =
                fetchedBuildStep.Variables.SingleOrDefault(o => o.Name == StepVariables.Profile.Name);
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
        public async Task It_Should_Get_StepVariable_By_VariableName()
        {
            // Arrange
            var profileVariable = StepVariables.Profile;
            profileVariable.StepId = 123;

            await Sut.CreateOrUpdateStepVariable(profileVariable);

            // Act
            var fetchedVariable = await Sut.GetStepVariable(123, profileVariable.Name);

            // Assert
            fetchedVariable.Should().BeEquivalentTo(profileVariable);
        }
        
        [Test]
        public async Task It_Should_Get_All_StepVariables()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.Name = "123";

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
        public async Task It_Should_Return_Step_Variable_Exist()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var variable = buildStep.Variables[0];

            // Act
            var doesStepExist = await Sut.DoesStepVariableExist(buildStep.BatchId, buildStep.Name, variable.Name);

            // Assert
            doesStepExist.Should().BeTrue();
        }

        [Test]
        public async Task It_Should_Return_Step_Variable_Does_Not_Exist_When_BatchId_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var variable = buildStep.Variables[0];

            // Act
            var doesStepExist = await Sut.DoesStepVariableExist("BatchId_1", buildStep.Name, variable.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Variable_Does_Not_Exist_When_StepName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var variable = buildStep.Variables[0];

            // Act
            var doesStepExist = await Sut.DoesStepVariableExist(buildStep.BatchId, "StepName_1", variable.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Variable_Does_Not_Exist_When_VariableName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            // Act
            var doesStepExist = await Sut.DoesStepVariableExist(buildStep.BatchId, buildStep.Name, "VariableName_1");

            // Assert
            doesStepExist.Should().BeFalse();
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
                fetchedBuildStep.Options.SingleOrDefault(o => o.Name == buildWhenNoErrorsOption.Name);
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

            var buildAlwaysOption = buildStep.Options.Single(o => o.Name == StepOptions.BuildAlways.Name);
            buildAlwaysOption.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateStepOption(buildAlwaysOption);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Options.Count.Should().Be(1);

            var fetchedBuildAlwaysOption =
                fetchedBuildStep.Options.SingleOrDefault(o => o.Name == StepOptions.BuildAlways.Name);
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
        public async Task It_Should_Get_StepOption_By_OptionName()
        {
            // Arrange
            var buildAlwaysOption = StepOptions.BuildAlways;
            buildAlwaysOption.StepId = 123;

            await Sut.CreateOrUpdateStepOption(buildAlwaysOption);

            // Act
            var fetchedOption = await Sut.GetStepOption(123, buildAlwaysOption.Name);

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
        public async Task It_Should_Return_Step_Option_Exist()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var option = buildStep.Options[0];

            // Act
            var doesStepExist = await Sut.DoesStepOptionExist(buildStep.BatchId, buildStep.Name, option.Name);

            // Assert
            doesStepExist.Should().BeTrue();
        }

        [Test]
        public async Task It_Should_Return_Step_Option_Does_Not_Exist_When_BatchId_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var option = buildStep.Options[0];

            // Act
            var doesStepExist = await Sut.DoesStepOptionExist("BatchId_1", buildStep.Name, option.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Option_Does_Not_Exist_When_StepName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var option = buildStep.Options[0];

            // Act
            var doesStepExist = await Sut.DoesStepOptionExist(buildStep.BatchId, "StepName_1", option.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Option_Does_Not_Exist_When_OptionName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            // Act
            var doesStepExist = await Sut.DoesStepOptionExist(buildStep.BatchId, buildStep.Name, "OptionName_1");

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Create_StepArtifact()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.Name = "123";

            var id = await Sut.Create(buildStep);

            var testResultsArtifact = StepArtifacts.TestResults;
            testResultsArtifact.StepId = id;

            // Act
            await Sut.CreateStepArtifact(testResultsArtifact);

            // Assert
            var fetchedStep = await Sut.Get(id);
            fetchedStep.Artifacts.Count.Should().Be(3);

            var fetchedTestResultsArtifact =
                fetchedStep.Artifacts.SingleOrDefault(o => o.Name == testResultsArtifact.Name);
            fetchedTestResultsArtifact.Should().NotBeNull();
            fetchedTestResultsArtifact.Should().BeEquivalentTo(testResultsArtifact);
        }
        
        [Test]
        public async Task It_Should_Update_StepArtifact()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.Name = "123";

            var id = await Sut.Create(buildStep);

            var sourceArtifact = buildStep.Artifacts.Single(o => o.Name == StepArtifacts.Source.Name);
            sourceArtifact.Description = "Updated";

            // Act
            await Sut.UpdateStepArtifact(sourceArtifact);

            // Assert
            var fetchedBuildStep = await Sut.Get(id);
            fetchedBuildStep.Artifacts.Count.Should().Be(2);

            var fetchedSourceArtifact =
                fetchedBuildStep.Artifacts.SingleOrDefault(o => o.Name == StepArtifacts.Source.Name);
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
        public async Task It_Should_Get_StepArtifact_By_ArtifactName()
        {
            // Arrange
            var binariesArtifact = StepArtifacts.Binaries;
            binariesArtifact.StepId = 123;

            await Sut.CreateStepArtifact(binariesArtifact);

            // Act
            var fetchedArtifact = await Sut.GetStepArtifact(123, binariesArtifact.Name);

            // Assert
            fetchedArtifact.Should().BeEquivalentTo(binariesArtifact);
        }
        
        [Test]
        public async Task It_Should_Get_All_StepArtifacts()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.Name = "123";

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
        public async Task It_Should_Return_Step_Artifact_Exist()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var artifact = buildStep.Artifacts[0];

            // Act
            var doesStepExist = await Sut.DoesStepArtifactExist(buildStep.BatchId, buildStep.Name, artifact.Name);

            // Assert
            doesStepExist.Should().BeTrue();
        }

        [Test]
        public async Task It_Should_Return_Step_Artifact_Does_Not_Exist_When_BatchId_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var artifact = buildStep.Artifacts[0];

            // Act
            var doesStepExist = await Sut.DoesStepArtifactExist("BatchId_1", buildStep.Name, artifact.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Artifact_Does_Not_Exist_When_StepName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var artifact = buildStep.Artifacts[0];

            // Act
            var doesStepExist = await Sut.DoesStepArtifactExist(buildStep.BatchId, "StepName_1", artifact.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Artifact_Does_Not_Exist_When_ArtifactName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            // Act
            var doesStepExist = await Sut.DoesStepArtifactExist(buildStep.BatchId, buildStep.Name, "ArtifactName_1");

            // Assert
            doesStepExist.Should().BeFalse();
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
        public async Task It_Should_Get_StepArtifactOption_By_OptionName()
        {
            // Arrange
            var compressOption = StepArtifactOptions.Compress;
            compressOption.StepArtifactId = 123;

            await Sut.CreateOrUpdateStepArtifactOption(compressOption);

            // Act
            var fetchedOption = await Sut.GetStepArtifactOption(123, compressOption.Name);

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
            var fetchedExtractOption = allOptions.SingleOrDefault(o => o.Name == extractOption.Name);
            fetchedExtractOption.Should().NotBeNull();
            fetchedExtractOption.Should().BeEquivalentTo(extractOption);
            var fetchedCompressOption = allOptions.SingleOrDefault(o => o.Name == compressOption.Name);
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

        [Test]
        public async Task It_Should_Return_Step_Artifact_Option_Exist()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var artifact = buildStep.Artifacts[0];
            var option = artifact.Options[0];

            // Act
            var doesStepExist = await Sut.DoesStepArtifactOptionExist(buildStep.BatchId, buildStep.Name, artifact.Name, option.Name);

            // Assert
            doesStepExist.Should().BeTrue();
        }

        [Test]
        public async Task It_Should_Return_Step_Artifact_Option_Does_Not_Exist_When_BatchId_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var artifact = buildStep.Artifacts[0];
            var option = artifact.Options[0];

            // Act
            var doesStepExist = await Sut.DoesStepArtifactOptionExist("BatchId_1", buildStep.Name, artifact.Name, option.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Artifact_Option_Does_Not_Exist_When_StepName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var artifact = buildStep.Artifacts[0];
            var option = artifact.Options[0];

            // Act
            var doesStepExist = await Sut.DoesStepArtifactOptionExist(buildStep.BatchId, "StepName_1", artifact.Name, option.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Artifact_Option_Does_Not_Exist_When_ArtifactName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var artifact = buildStep.Artifacts[0];
            var option = artifact.Options[0];

            // Act
            var doesStepExist = await Sut.DoesStepArtifactOptionExist(buildStep.BatchId, buildStep.Name, "ArtifactName_1", option.Name);

            // Assert
            doesStepExist.Should().BeFalse();
        }

        [Test]
        public async Task It_Should_Return_Step_Artifact_Option_Does_Not_Exist_When_OptionName_Is_Incorrect()
        {
            // Arrange
            var buildStep = Steps.Build;
            buildStep.BatchId = "123";
            await Sut.Create(buildStep);

            var artifact = buildStep.Artifacts[0];

            // Act
            var doesStepExist = await Sut.DoesStepArtifactOptionExist(buildStep.BatchId, buildStep.Name, artifact.Name, "OptionName_1");

            // Assert
            doesStepExist.Should().BeFalse();
        }
    }
}