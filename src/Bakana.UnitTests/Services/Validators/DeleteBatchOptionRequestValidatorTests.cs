using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class DeleteBatchOptionRequestValidatorTests
    {
        private DeleteBatchOptionRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DeleteBatchOptionRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new DeleteBatchOptionRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(DeleteBatchOptionRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(DeleteBatchOptionRequest.OptionName) && r.ErrorMessage == "'Option Name' must not be empty.");
        }
    }
}