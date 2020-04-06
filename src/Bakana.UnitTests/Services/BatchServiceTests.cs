using System.Net;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceInterface;
using Bakana.ServiceInterface.Batches;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;

namespace Bakana.UnitTests.Services
{
    [TestFixture]
    public class BatchServiceTests
    {
        private const string TestBatchId = "TestBatch";
        
        private readonly ServiceStackHost appHost;
        private readonly IBatchRepository batchRepository;
        private readonly IShortIdGenerator shortIdGenerator;
        private BatchService Sut { get; set; }

        public BatchServiceTests()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<BatchService>();

            batchRepository = Substitute.For<IBatchRepository>();
            appHost.Container.AddTransient(() => batchRepository);

            shortIdGenerator = Substitute.For<IShortIdGenerator>();
            appHost.Container.AddTransient(() => shortIdGenerator);
        }
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Sut = appHost.Resolve<BatchService>();
            
            Mappers.Register();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public async Task It_Should_Create_Batch()
        {
            // Arrange
            shortIdGenerator.Generate().Returns(TestBatchId);

            var request = new CreateBatchRequest();

            // Act
            var response = await Sut.Post(request);

            // Assert
            response.BatchId.Should().Be(TestBatchId);
        }

        [Test]
        public async Task It_Should_Get_Batch()
        {
            // Arrange
            batchRepository.Get(TestBatchId).Returns(new Batch
            {
                Id = TestBatchId,
                Description = "TEST"
            });
            
            var request = new GetBatchRequest
            {
                BatchId = TestBatchId
            };

            // Act
            var response = await Sut.Get(request);

            // Assert
            response.BatchId.Should().Be(TestBatchId);
        }
        
        [Test]
        public void Get_Batch_Should_Throw_With_Invalid_Batch_Id()
        {
            // Arrange
            var request = new GetBatchRequest
            {
                BatchId = TestBatchId
            };

            // Act / Assert
            var exception = Assert.ThrowsAsync<HttpError>(() => Sut.Get(request));
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound.ToString());
            exception.Message.Should().Be("Batch TestBatch not found");
        }
    }
}