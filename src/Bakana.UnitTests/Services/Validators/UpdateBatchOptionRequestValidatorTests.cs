using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class UpdateBatchOptionRequestValidatorTests
    {
        private UpdateBatchOptionRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateBatchOptionRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new UpdateBatchOptionRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchOptionRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchOptionRequest.OptionName) && r.ErrorMessage == "'Option Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchOptionRequest.Value) && r.ErrorMessage == "'Value' must not be empty.");
        }
    }
}