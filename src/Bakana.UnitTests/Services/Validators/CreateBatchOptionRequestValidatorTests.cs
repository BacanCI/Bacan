using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class CreateBatchOptionRequestValidatorTests
    {
        private CreateBatchOptionRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CreateBatchOptionRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new CreateBatchOptionRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchOptionRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchOptionRequest.OptionName) && r.ErrorMessage == "'Option Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchOptionRequest.Value) && r.ErrorMessage == "'Value' must not be empty.");
        }
    }
}