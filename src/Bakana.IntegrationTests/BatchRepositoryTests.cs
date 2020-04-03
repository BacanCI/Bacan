using System.Linq;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.TestData.Entities;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.OrmLite;

namespace Bakana.IntegrationTests
{
    [TestFixture]
    public class BatchRepositoryTests : RepositoryTestFixtureBase
    {
        private IBatchRepository Sut { get; set; }
        
        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();

            Sut = new BatchRepository(DbConnectionFactory);
        }

        [Test]
        public async Task It_Should_Create()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            // Act
            await Sut.Create(fullyPopulatedBatch);

            // Assert
            var fetchedBuildBatch = await Sut.Get(fullyPopulatedBatch.Id);
            fetchedBuildBatch.Should().BeEquivalentTo(fetchedBuildBatch);
        }

        [Test]
        public async Task It_Should_Update()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            var fetchedBatch = await Sut.Get(fullyPopulatedBatch.Id);

            // Act
            fetchedBatch.Description = "Updated1";
            await Sut.Update(fetchedBatch);

            // Assert
            var updatedBatch = await Sut.Get(fullyPopulatedBatch.Id);
            updatedBatch.Should().BeEquivalentTo(fetchedBatch);
        }
        
        [Test]
        public async Task It_Should_Delete()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);
            
            // Act
            await Sut.Delete(fullyPopulatedBatch.Id);
            
            // Assert
            using (var db = await DbConnectionFactory.OpenDbConnectionAsync())
            {
                var q = db.From<Batch>()
                    .Select(Sql.Count("*"));
                var batches = await db.ScalarAsync<int>(q);
                batches.Should().Be(0);
            }
        }
        
        [Test]
        public async Task It_Should_Get()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            // Act
            var fetchedBatch = await Sut.Get(fullyPopulatedBatch.Id);

            // Assert
            fetchedBatch.Should().BeEquivalentTo(fullyPopulatedBatch, o => o.Excluding(b => b.CreatedOn));
        }
        
        [Test]
        public async Task It_Should_UpdateState()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            // Act
            await Sut.UpdateState(fullyPopulatedBatch.Id, BatchState.Stopped);

            // Assert
            var fetchedBatch = await Sut.Get(fullyPopulatedBatch.Id);
            fetchedBatch.State.Should().BeEquivalentTo(BatchState.Stopped);
            fetchedBatch.Should().BeEquivalentTo(fullyPopulatedBatch, o => o
                .Excluding(c => c.State)
                .Excluding(c => c.CreatedOn));
        }
        
        [Test]
        public async Task It_Should_Create_BatchVariable()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            var environmentVariable = BatchVariables.Environment;
            environmentVariable.BatchId = fullyPopulatedBatch.Id;

            // Act
            await Sut.CreateOrUpdateBatchVariable(environmentVariable);

            // Assert
            var fetchedBatch = await Sut.Get(fullyPopulatedBatch.Id);
            fetchedBatch.Variables.Count.Should().Be(2);

            var fetchedEnvironmentVariable =
                fetchedBatch.Variables.SingleOrDefault(o => o.VariableId == environmentVariable.VariableId);
            fetchedEnvironmentVariable.Should().NotBeNull();
            fetchedEnvironmentVariable.Should().BeEquivalentTo(environmentVariable);
        }
        
        [Test]
        public async Task It_Should_Update_BatchVariable()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            var scheduleVariable = fullyPopulatedBatch.Variables.Single(o => o.VariableId == BatchVariables.Schedule.VariableId);
            scheduleVariable.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateBatchVariable(scheduleVariable);

            // Assert
            var fetchedBatch = await Sut.Get(fullyPopulatedBatch.Id);
            fetchedBatch.Variables.Count.Should().Be(1);

            var fetchedScheduleVariable =
                fetchedBatch.Variables.SingleOrDefault(o => o.VariableId == BatchVariables.Schedule.VariableId);
            fetchedScheduleVariable.Should().NotBeNull();
            fetchedScheduleVariable.Should().BeEquivalentTo(scheduleVariable);
        }
        
        [Test]
        public async Task It_Should_Get_BatchVariable()
        {
            // Arrange
            var scheduleVariable = BatchVariables.Schedule;
            scheduleVariable.BatchId = "123";

            var id = await Sut.CreateOrUpdateBatchVariable(scheduleVariable);

            // Act
            var fetchedVariable = await Sut.GetBatchVariable(id);

            // Assert
            fetchedVariable.Should().BeEquivalentTo(scheduleVariable);
        }
        
        [Test]
        public async Task It_Should_Get_BatchVariable_By_VariableId()
        {
            // Arrange
            var scheduleVariable = BatchVariables.Schedule;
            scheduleVariable.BatchId = "123";

            await Sut.CreateOrUpdateBatchVariable(scheduleVariable);

            // Act
            var fetchedVariable = await Sut.GetBatchVariable("123", scheduleVariable.VariableId);

            // Assert
            fetchedVariable.Should().BeEquivalentTo(scheduleVariable);
        }
        
        [Test]
        public async Task It_Should_Get_All_BatchVariables()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            // Act
            var fetchedBatchVariables = await Sut.GetAllBatchVariables(fullyPopulatedBatch.Id);

            // Assert
            fetchedBatchVariables.Count.Should().Be(1);
            fetchedBatchVariables.Should().BeEquivalentTo(fullyPopulatedBatch.Variables);
        }
        
        [Test]
        public async Task It_Should_Delete_BatchVariable()
        {
            // Arrange
            var scheduleVariable = BatchVariables.Schedule;
            scheduleVariable.BatchId = "123";

            var id = await Sut.CreateOrUpdateBatchVariable(scheduleVariable);

            // Act
            await Sut.DeleteBatchVariable(id);

            // Assert
            var fetchedVariable = await Sut.GetBatchVariable(id);
            fetchedVariable.Should().BeNull();
        }
        
        [Test]
        public async Task It_Should_Create_BatchOption()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            var logOption = BatchOptions.Log;
            logOption.BatchId = fullyPopulatedBatch.Id;

            // Act
            await Sut.CreateOrUpdateBatchOption(logOption);

            // Assert
            var fetchedBuildBatch = await Sut.Get(fullyPopulatedBatch.Id);
            fetchedBuildBatch.Options.Count.Should().Be(2);

            var fetchedLogOption =
                fetchedBuildBatch.Options.SingleOrDefault(o => o.OptionId == logOption.OptionId);
            fetchedLogOption.Should().NotBeNull();
            fetchedLogOption.Should().BeEquivalentTo(logOption);
        }
        
        [Test]
        public async Task It_Should_Update_BatchOption()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            var debugOption = fullyPopulatedBatch.Options.Single(o => o.OptionId == BatchOptions.Debug.OptionId);
            debugOption.Description = "Updated";

            // Act
            await Sut.CreateOrUpdateBatchOption(debugOption);

            // Assert
            var fetchedBatch = await Sut.Get(fullyPopulatedBatch.Id);
            fetchedBatch.Options.Count.Should().Be(1);

            var fetchedDebugOption =
                fetchedBatch.Options.SingleOrDefault(o => o.OptionId == BatchOptions.Debug.OptionId);
            fetchedDebugOption.Should().NotBeNull();
            fetchedDebugOption.Should().BeEquivalentTo(debugOption);
        }
        
        [Test]
        public async Task It_Should_Get_BatchOption()
        {
            // Arrange
            var debugOption = BatchOptions.Debug;
            debugOption.BatchId = "123";

            var id = await Sut.CreateOrUpdateBatchOption(debugOption);

            // Act
            var fetchedOption = await Sut.GetBatchOption(id);

            // Assert
            fetchedOption.Should().BeEquivalentTo(debugOption);
        }
        
        [Test]
        public async Task It_Should_Get_BatchOption_By_OptionId()
        {
            // Arrange
            var debugOption = BatchOptions.Debug;
            debugOption.BatchId = "123";

            await Sut.CreateOrUpdateBatchOption(debugOption);

            // Act
            var fetchedOption = await Sut.GetBatchOption("123", debugOption.OptionId);

            // Assert
            fetchedOption.Should().BeEquivalentTo(debugOption);
        }
        
        [Test]
        public async Task It_Should_Get_All_BatchOptions()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            // Act
            var fetchedBatchOptions = await Sut.GetAllBatchOptions(fullyPopulatedBatch.Id); 

            // Assert
            fetchedBatchOptions.Count.Should().Be(1);
            fetchedBatchOptions.Should().BeEquivalentTo(fullyPopulatedBatch.Options);
        }
        
        [Test]
        public async Task It_Should_Delete_BatchOption()
        {
            // Arrange
            var debugOption = BatchOptions.Debug;
            debugOption.BatchId = "123";

            var id = await Sut.CreateOrUpdateBatchOption(debugOption);

            // Act
            await Sut.DeleteBatchOption(id);

            // Assert
            var fetchedOption = await Sut.GetBatchOption(id);
            fetchedOption.Should().BeNull();
        }
        
        [Test]
        public async Task It_Should_Create_BatchArtifact()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            var dbBackupArtifact = BatchArtifacts.DbBackup;
            dbBackupArtifact.BatchId = fullyPopulatedBatch.Id;

            // Act
            await Sut.CreateBatchArtifact(dbBackupArtifact);

            // Assert
            var fetchedBatch = await Sut.Get(fullyPopulatedBatch.Id);
            fetchedBatch.InputArtifacts.Count.Should().Be(2);

            var fetchedDbBackupArtifact =
                fetchedBatch.InputArtifacts.SingleOrDefault(o => o.ArtifactId == dbBackupArtifact.ArtifactId);
            fetchedDbBackupArtifact.Should().NotBeNull();
            fetchedDbBackupArtifact.Should().BeEquivalentTo(dbBackupArtifact);
        }
        
        [Test]
        public async Task It_Should_Update_BatchArtifact()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            var packageArtifact = fullyPopulatedBatch.InputArtifacts.Single(o => o.ArtifactId == BatchArtifacts.Package.ArtifactId);
            packageArtifact.Description = "Updated";

            // Act
            await Sut.UpdateBatchArtifact(packageArtifact);

            // Assert
            var fetchedBuildBatch = await Sut.Get(fullyPopulatedBatch.Id);
            fetchedBuildBatch.InputArtifacts.Count.Should().Be(1);

            var fetchedPackageArtifact =
                fetchedBuildBatch.InputArtifacts.SingleOrDefault(o => o.ArtifactId == BatchArtifacts.Package.ArtifactId);
            fetchedPackageArtifact.Should().NotBeNull();
            fetchedPackageArtifact.Should().BeEquivalentTo(packageArtifact);
        }
        
        [Test]
        public async Task It_Should_Get_BatchArtifact()
        {
            // Arrange
            var packageArtifact = BatchArtifacts.Package;
            packageArtifact.BatchId = "123";

            var id = await Sut.CreateBatchArtifact(packageArtifact);

            // Act
            var fetchedArtifact = await Sut.GetBatchArtifact(id);

            // Assert
            fetchedArtifact.Should().BeEquivalentTo(packageArtifact);
        }
        
        [Test]
        public async Task It_Should_Get_BatchArtifact_By_ArtifactId()
        {
            // Arrange
            var packageArtifact = BatchArtifacts.Package;
            packageArtifact.BatchId = "123";

            await Sut.CreateBatchArtifact(packageArtifact);

            // Act
            var fetchedArtifact = await Sut.GetBatchArtifact("123", packageArtifact.ArtifactId);

            // Assert
            fetchedArtifact.Should().BeEquivalentTo(packageArtifact);
        }
        
        [Test]
        public async Task It_Should_Get_All_BatchArtifacts()
        {
            // Arrange
            var fullyPopulatedBatch = Batches.FullyPopulated;
            fullyPopulatedBatch.Id = "123";

            await Sut.Create(fullyPopulatedBatch);

            // Act
            var fetchedArtifacts = await Sut.GetAllBatchArtifacts(fullyPopulatedBatch.Id);

            // Assert
            fetchedArtifacts.Count.Should().Be(1);
            fetchedArtifacts.Single().Should().BeEquivalentTo(fullyPopulatedBatch.InputArtifacts.Single());
        }
        
        [Test]
        public async Task It_Should_Delete_BatchArtifact()
        {
            // Arrange
            var packageArtifact = BatchArtifacts.Package;
            packageArtifact.BatchId = "123";

            var id = await Sut.CreateBatchArtifact(packageArtifact);

            // Act
            await Sut.DeleteBatchArtifact(id);

            // Assert
            var fetchedVariable = await Sut.GetBatchArtifact(id);
            fetchedVariable.Should().BeNull();
        }
        
        [Test]
        public async Task It_Should_Create_BatchArtifactOption()
        {
            // Arrange
            var compressOption = BatchArtifactOptions.Compress;
            compressOption.BatchArtifactId = 123;

            // Act
            var id = await Sut.CreateOrUpdateBatchArtifactOption(compressOption);

            // Assert
            var fetchedArtifactOption = await Sut.GetBatchArtifactOption(id);
            fetchedArtifactOption.Should().BeEquivalentTo(compressOption);
        }
        
        [Test]
        public async Task It_Should_Update_BatchArtifactOption()
        {
            // Arrange
            var compressOption = BatchArtifactOptions.Compress;
            compressOption.BatchArtifactId = 123;

            var id = await Sut.CreateOrUpdateBatchArtifactOption(compressOption);

            var fetchedOption = await Sut.GetBatchArtifactOption(id);
            fetchedOption.Description = "Updated";
            
            // Act
            await Sut.CreateOrUpdateBatchArtifactOption(fetchedOption);

            // Assert
            var updatedOption = await Sut.GetBatchArtifactOption(id);
            updatedOption.Should().BeEquivalentTo(fetchedOption);
        }
        
        [Test]
        public async Task It_Should_Get_BatchArtifactOption()
        {
            // Arrange
            var compressOption = BatchArtifactOptions.Compress;
            compressOption.BatchArtifactId = 123;

            var id = await Sut.CreateOrUpdateBatchArtifactOption(compressOption);

            // Act
            var fetchedOption = await Sut.GetBatchArtifactOption(id);

            // Assert
            fetchedOption.Should().BeEquivalentTo(compressOption);
        }
        
        [Test]
        public async Task It_Should_Get_BatchArtifactOption_By_OptionId()
        {
            // Arrange
            var compressOption = BatchArtifactOptions.Compress;
            compressOption.BatchArtifactId = 123;

            await Sut.CreateOrUpdateBatchArtifactOption(compressOption);

            // Act
            var fetchedOption = await Sut.GetBatchArtifactOption(123, compressOption.OptionId);

            // Assert
            fetchedOption.Should().BeEquivalentTo(compressOption);
        }
        
        [Test]
        public async Task It_Should_Get_All_BatchArtifactOptions()
        {
            // Arrange
            var extractOption = BatchArtifactOptions.Extract;
            extractOption.BatchArtifactId = 123;
            await Sut.CreateOrUpdateBatchArtifactOption(extractOption);

            var compressOption = BatchArtifactOptions.Compress;
            compressOption.BatchArtifactId = 123;
            await Sut.CreateOrUpdateBatchArtifactOption(compressOption);

            // Act
            var allOptions = await Sut.GetAllBatchArtifactOptions(123);

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
        public async Task It_Should_Delete_BatchArtifactOption()
        {
            // Arrange
            var compressOption = BatchArtifactOptions.Compress;
            compressOption.BatchArtifactId = 123;

            var id = await Sut.CreateOrUpdateBatchArtifactOption(compressOption);

            // Act
            await Sut.DeleteBatchArtifactOption(id);

            // Assert
            var fetchedOption = await Sut.GetBatchArtifactOption(id);
            fetchedOption.Should().BeNull();
        }
    }
}