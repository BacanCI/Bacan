using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class DeleteBatchRequestValidatorTests
    {
        private DeleteBatchRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DeleteBatchRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new DeleteBatchRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(DeleteBatchRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
        }
    }
}