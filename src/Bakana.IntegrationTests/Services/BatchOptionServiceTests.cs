using System.Net;
using Bakana.ServiceInterface;
using Bakana.ServiceInterface.Batches;
using Bakana.ServiceModels.Batches;
using Bakana.TestData.ServiceModels;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;

namespace Bakana.IntegrationTests.Services
{
    [TestFixture]
    public class BatchOptionServiceTests : ServiceTestFixtureBase<BatchOptionService>
    {
        protected CreateBatchRequest BatchRequest => CreateBatches.FullyPopulated;

        [SetUp]
        public void Setup()
        {
        }

        protected string CreateBatch()
        {
            var response = Sut.Post(BatchRequest);
            response.BatchId.Should().NotBeEmpty();
            return response.BatchId;
        }

        protected CreateBatchOptionRequest CreateBatchOptionRequest(string batchId)
        {
            var batchOption = CreateBatchOptions.Log;
            batchOption.BatchId = batchId;
            return batchOption;
        }

        protected UpdateBatchOptionRequest UpdateBatchOptionRequest(string batchId)
        {
            var batchOption = UpdateBatchOptions.Log;
            batchOption.BatchId = batchId;
            return batchOption;
        }

        protected DeleteBatchOptionRequest DeleteBatchOptionRequest(string batchId)
        {
            var batchOption = DeleteBatchOptions.Log;
            batchOption.BatchId = batchId;
            return batchOption;
        }

        protected GetBatchOptionRequest GetBatchOptionRequest(string batchId)
        {
            var batchOption = GetBatchOptions.Log;
            batchOption.BatchId = batchId;
            return batchOption;
        }

        [Test]
        public void It_Should_Create_Batch_Option()
        {
            // Arrange - CreateBatch creates Debug option
            var batchId = CreateBatch();
            //Create Log option
            var request = CreateBatchOptionRequest(batchId);

            // Act
            var response = Sut.Post(request);
            
            // Assert
            response.Should().NotBeNull();
            var getBatchOptionResponse = Sut.Get(GetBatchOptionRequest(batchId));
            getBatchOptionResponse.Should().BeEquivalentTo(request,
                options => options.ExcludingMissingMembers());

            //Assert all options have been returned
            var getAllBatchOptionResponse = Sut.Get(new GetAllBatchOptionRequest { BatchId = batchId });
            getAllBatchOptionResponse.Options.Should().NotBeNull();
            getAllBatchOptionResponse.Options.Count.Should().Be(2);
            getAllBatchOptionResponse.Options.Should()
                .ContainEquivalentOf(request, options => options.ExcludingMissingMembers());
            var debugRequest = CreateBatchOptions.Debug;
            debugRequest.BatchId = batchId;
            getAllBatchOptionResponse.Options.Should()
                .ContainEquivalentOf(debugRequest, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void It_Should_Throw_When_Creating_Option_When_Batch_Id_Does_Not_Exist()
        {
            var request = CreateBatchOptionRequest("invalid");

            try
            {
                Sut.Post(request);
                Assert.Fail("Should throw");
            }
            catch (WebServiceException webEx)
            {
                webEx.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
                webEx.Message.Should().Be(ErrMsg.BatchNotFound(request.BatchId));
            }
        }

        [Test]
        public void It_Should_Throw_When_Creating_Option_When_Option_Already_Exists()
        {
            var batchId = CreateBatch();
            var request = CreateBatchOptionRequest(batchId);
            var response = Sut.Post(request);
            response.Should().NotBeNull();

            try
            {
                Sut.Post(request);
                Assert.Fail("Should throw");
            }
            catch (WebServiceException webEx)
            {
                webEx.StatusCode.Should().Be((int)HttpStatusCode.Conflict);
                webEx.Message.Should().Be(ErrMsg.BatchOptionAlreadyExists(request.OptionName));
            }
        }

        [Test]
        public void It_Should_Throw_When_Getting_Option_When_Batch_Id_Does_Not_Exist()
        {
            var request = new GetBatchOptionRequest { BatchId = "invalid", OptionName = "invalid" };
            try
            {
                Sut.Get(request);
                Assert.Fail("Should throw");
            }
            catch (WebServiceException webEx)
            {
                webEx.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
                webEx.Message.Should().Be(ErrMsg.BatchNotFound(request.BatchId));
            }
        }

        [Test]
        public void It_Should_Throw_When_Getting_Option_When_Option_Name_Does_Not_Exist()
        {
            var batchId = CreateBatch();
            var request = new GetBatchOptionRequest { BatchId = batchId, OptionName = "invalid" };
            try
            {
                Sut.Get(request);
                Assert.Fail("Should throw");
            }
            catch (WebServiceException webEx)
            {
                webEx.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
                webEx.Message.Should().Be(ErrMsg.BatchOptionNotFound(request.OptionName));
            }
        }

        [Test]
        public void It_Should_Update_An_Existing_Batch_Option()
        {
            // Arrange
            var batchId = CreateBatch();
            var createBatchOptionRequest = CreateBatchOptionRequest(batchId);
            var createBatchOptionResponse = Sut.Post(createBatchOptionRequest);
            createBatchOptionResponse.Should().NotBeNull();
            var request = UpdateBatchOptionRequest(batchId);

            // Act
            var response = Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            var getBatchOptionResponse = Sut.Get(GetBatchOptionRequest(batchId));
            getBatchOptionResponse.Should().BeEquivalentTo(request,
                options => options.Excluding(r => r.BatchId));
        }

        [Ignore("Is PUT an upsert? batchRepository.CreateOrUpdateBatchOption")]
        [Test]
        public void It_Should_Upsert_A_Batch_Option()
        {
            // Arrange
            var batchId = CreateBatch();
            var request = UpdateBatchOptionRequest(batchId);

            // Act
            var response = Sut.Put(request);

            // Assert
            response.Should().NotBeNull();
            var getBatchOptionResponse = Sut.Get(GetBatchOptionRequest(batchId));
            getBatchOptionResponse.Should().BeEquivalentTo(request,
                options => options.Excluding(r => r.BatchId));
        }

        [Test]
        public void It_Should_Delete_Batch_Option()
        {
            // Arrange
            var batchId = CreateBatch();
            var createBatchOptionRequest = CreateBatchOptionRequest(batchId);
            var createBatchOptionResponse = Sut.Post(createBatchOptionRequest);
            createBatchOptionResponse.Should().NotBeNull();
            var request = DeleteBatchOptionRequest(batchId);

            // Act
            var response = Sut.Delete(request);

            // Assert
            response.Should().NotBeNull();

            try
            {
                Sut.Get(GetBatchOptionRequest(batchId));
                Assert.Fail("Batch should not exist");
            }
            catch (WebServiceException webEx)
            {
                webEx.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
                webEx.Message.Should().Be(ErrMsg.BatchOptionNotFound(request.OptionName));
            }
        }
    }
}