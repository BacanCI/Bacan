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
using StepArtifacts = Bakana.TestData.Entities.StepArtifacts;

namespace Bakana.UnitTests.Services.Steps
{
    public class StepArtifactServiceTests : ServiceTestFixtureBase<StepArtifactService>
    {
        private const string TestBatchId = "TestBatch";
        private const string TestStepName = "TestStep";
        private const string TestStepArtifactName = "TestStepArtifact";
        private const string TestStepArtifactOptionName = "TestStepArtifactOption";
        
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
        public async Task It_Should_Create_StepArtifact()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step
            {
                Id = 123
            });
            
            stepRepository.DoesStepArtifactExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);
            
            var request = CreateStepArtifacts.Binaries;
            request.BatchId = TestBatchId;
            request.StepName = TestStepName;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().CreateStepArtifact(Arg.Is<StepArtifact>(a =>
                a.StepId == 123 &&
                a.Name == request.ArtifactName &&
                a.Description == request.Description &&
                a.Options.Count == request.Options.Count));
        }

        [Test]
        public void Create_StepArtifact_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateStepArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Create_StepArtifact_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new CreateStepArtifactRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Create_StepArtifact_Should_Throw_With_Existing_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.DoesStepArtifactExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateStepArtifactRequest
            {
                ArtifactName = TestStepArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact already exists");
        }
        
        [Test]
        public async Task It_Should_Get_StepArtifact()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step
            {
                Id = 123
            });
            
            var stepArtifact = StepArtifacts.Binaries;
            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(stepArtifact);

            var request = new GetStepArtifactRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Should().BeEquivalentTo(TestData.ServiceModels.StepArtifacts.Binaries, 
                o => o.ExcludingMissingMembers());
            response.ArtifactName.Should().Be(TestData.ServiceModels.StepArtifacts.Binaries.Name);
        }
        
        [Test]
        public void Get_StepArtifact_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetStepArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_StepArtifact_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new GetStepArtifactRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Get_StepArtifact_Should_Throw_With_Invalid_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetStepArtifactRequest
            {
                ArtifactName = TestStepArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_StepArtifact()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);
            
            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step
            {
                Id = 123
            });

            var stepArtifacts = TestData.Entities.Steps.Build.Artifacts;
            stepRepository.GetAllStepArtifacts(Arg.Any<ulong>())
                .Returns(stepArtifacts);

            var request = new GetAllStepArtifactRequest();

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.Artifacts.Should().BeEquivalentTo(TestData.ServiceModels.Steps.Build.Artifacts);
        }
        
        [Test]
        public void Get_All_StepArtifacts_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllStepArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_All_StepArtifacts_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllStepArtifactRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public async Task It_Should_Update_StepArtifact()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step
                {
                    Id = 123
                });

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact
                {
                    Id = 456
                });
            
            var request = UpdateStepArtifacts.Binaries;
            request.StepName = TestStepName;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().UpdateStepArtifact(Arg.Is<StepArtifact>(a =>
                a.Id == 456 && 
                a.StepId == 123 &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_StepArtifact_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateStepArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Update_StepArtifact_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new UpdateStepArtifactRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Update_StepArtifact_Should_Throw_With_Invalid_Step_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>()).ReturnsNull();

            var request = new UpdateStepArtifactRequest
            {
                ArtifactName = TestStepArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Step_Artifact()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            var existingStepArtifact = new StepArtifact
            {
                Id = 123
            };
            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(existingStepArtifact);

            var request = new DeleteStepArtifactRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().DeleteStepArtifact(Arg.Is<ulong>(a =>
                a == existingStepArtifact.Id));
        }
        
        [Test]
        public void Delete_Step_Artifact_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteStepArtifactRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Delete_Step_Artifact_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new DeleteStepArtifactRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }

        [Test]
        public void Delete_Step_Artifact_Should_Throw_With_Invalid_Step_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteStepArtifactRequest
            {
                ArtifactName = TestStepArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact not found");
        }
        
        [Test]
        public async Task It_Should_Create_StepArtifact_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());
            
            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>()).Returns(new StepArtifact
            {
                Id = 123
            });
            
            stepRepository.DoesStepArtifactOptionExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);

            var request = CreateStepArtifactOptions.Compress;

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().CreateOrUpdateStepArtifactOption(Arg.Is<StepArtifactOption>(a =>
                a.StepArtifactId == 123 &&
                a.Name == request.OptionName &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Create_Step_Artifact_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new CreateStepArtifactOptionRequest
            {
                BatchId = TestBatchId
            };

            // Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Create_Step_Artifact_Option_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new CreateStepArtifactOptionRequest
            {
                StepName = TestStepName
            };

            // Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Create_Step_Artifact_Option_Should_Throw_With_Invalid_Step_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>()).ReturnsNull();

            var request = new CreateStepArtifactOptionRequest
            {
                ArtifactName = TestStepArtifactName
            };

            // Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact not found");
        }
        
        [Test]
        public void Create_Step_Artifact_Option_Should_Throw_With_Existing_Step_Artifact_Option_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact());

            stepRepository.DoesStepArtifactOptionExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var request = new CreateStepArtifactOptionRequest
            {
                OptionName = TestStepArtifactOptionName
            };

            // Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Post(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict.ToString());
            exception.Message.Should().Be("Step Artifact Option TestStepArtifactOption already exists");
        }
        
        [Test]
        public async Task It_Should_Get_StepArtifact_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact());
            
            var stepArtifactOption = TestData.Entities.StepArtifactOptions.Extract;
            stepRepository.GetStepArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(stepArtifactOption);

            // Act
            var response = await Sut.Get(new GetStepArtifactOptionRequest());

            // Assert
            response.Should().BeEquivalentTo(StepArtifactOptions.Extract, 
                o => o.ExcludingMissingMembers());
            response.OptionName.Should().Be(StepArtifactOptions.Extract.Name);
        }
        
        [Test]
        public void Get_StepArtifact_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetStepArtifactOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_StepArtifact_Option_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            var request = new GetStepArtifactOptionRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Get_StepArtifact_Option_Should_Throw_With_Invalid_Step_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();
            
            var request = new GetStepArtifactOptionRequest
            {
                ArtifactName = TestStepArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact not found");
        }
        
        [Test]
        public void Get_StepArtifact_Option_Should_Throw_With_Invalid_Step_Artifact_Option_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact());

            stepRepository.GetStepArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetStepArtifactOptionRequest
            {
                OptionName = TestStepArtifactOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact Option TestStepArtifactOption not found");
        }
        
        [Test]
        public async Task It_Should_Get_All_StepArtifact_Options()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact());

            var stepArtifact = StepArtifacts.Binaries;
            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(stepArtifact);
            
            // Act
            var response = await Sut.Get(new GetAllStepArtifactOptionRequest());

            // Assert
            response.Options.Should().BeEquivalentTo(TestData.ServiceModels.StepArtifacts.Binaries.Options);
        }
        
        [Test]
        public void Get_All_StepArtifact_Options_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new GetAllStepArtifactOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Get_All_StepArtifact_Options_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllStepArtifactOptionRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Get_All_StepArtifact_Options_Should_Throw_With_Invalid_Step_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new GetAllStepArtifactOptionRequest
            {
                ArtifactName = TestStepArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact not found");
        }
        
        [Test]
        public async Task It_Should_Update_StepArtifact_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());
            
            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact
                {
                    Id = 123
                });

            var stepArtifactOption = new StepArtifactOption
            {
                Id = 456
            };
            stepRepository.GetStepArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(stepArtifactOption);

            var request = UpdateStepArtifactOptions.Compress;
            request.StepName = TestStepName;
            request.ArtifactName = TestStepArtifactName;

            // Act
            var response = await Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().CreateOrUpdateStepArtifactOption(Arg.Is<StepArtifactOption>(a =>
                a.Id == 456 &&
                a.StepArtifactId == 123 &&
                a.Name == request.OptionName &&
                a.Description == request.Description));
        }
        
        [Test]
        public void Update_StepArtifact_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new UpdateStepArtifactOptionRequest
            {
                BatchId = TestBatchId
            };
            
            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Update_StepArtifact_Option_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateStepArtifactOptionRequest
            {
                StepName = TestStepName
            };
            
            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Update_StepArtifact_Option_Should_Throw_With_Invalid_Step_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateStepArtifactOptionRequest
            {
                ArtifactName = TestStepArtifactName
            };
            
            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact not found");
        }
        
        [Test]
        public void Update_StepArtifact_Option_Should_Throw_With_Invalid_Step_Artifact_Option_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact());

            stepRepository.GetStepArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new UpdateStepArtifactOptionRequest
            {
                OptionName = TestStepArtifactOptionName
            };
            
            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Put(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact Option TestStepArtifactOption not found");
        }
        
        [Test]
        public async Task It_Should_Delete_Step_Artifact_Option()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact());

            var existingStepArtifactOption = new StepArtifactOption
            {
                Id = 123
            };
            stepRepository.GetStepArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(existingStepArtifactOption);

            stepRepository.DeleteStepArtifactOption(Arg.Any<ulong>()).Returns(true);

            var request = new DeleteStepArtifactOptionRequest();

            // Act
            var response = await Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();
            await stepRepository.Received().DeleteStepArtifactOption(Arg.Is<ulong>(a =>
                a == existingStepArtifactOption.Id));
        }
        
        [Test]
        public void Delete_Step_Artifact_Option_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(false);

            var request = new DeleteStepArtifactOptionRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }

        [Test]
        public void Delete_Step_Artifact_Option_Should_Throw_With_Invalid_Step_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteStepArtifactOptionRequest
            {
                StepName = TestStepName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step TestStep not found");
        }
        
        [Test]
        public void Delete_Step_Artifact_Option_Should_Throw_With_Invalid_Step_Artifact_Id()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteStepArtifactOptionRequest
            {
                ArtifactName = TestStepArtifactName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact TestStepArtifact not found");
        }
        
        [Test]
        public void Delete_Step_Artifact_Option_Should_Throw_With_Invalid_Step_Artifact_Option_Name()
        {
            // Arrange
            batchRepository.DoesBatchExist(Arg.Any<string>())
                .Returns(true);

            stepRepository.Get(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new Step());

            stepRepository.GetStepArtifact(Arg.Any<ulong>(), Arg.Any<string>())
                .Returns(new StepArtifact());

            stepRepository.GetStepArtifactOption(Arg.Any<ulong>(), Arg.Any<string>())
                .ReturnsNull();

            var request = new DeleteStepArtifactOptionRequest
            {
                OptionName = TestStepArtifactOptionName
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Delete(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Step Artifact Option TestStepArtifactOption not found");
        }
    }
}