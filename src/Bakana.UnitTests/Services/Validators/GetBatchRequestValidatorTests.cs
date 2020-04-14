using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class GetBatchRequestValidatorTests
    {
        private GetBatchRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetBatchRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new GetBatchRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(GetBatchRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
        }
    }
}