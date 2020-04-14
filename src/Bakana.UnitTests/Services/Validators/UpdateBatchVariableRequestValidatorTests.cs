using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class UpdateBatchVariableRequestValidatorTests
    {
        private UpdateBatchVariableRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateBatchVariableRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new UpdateBatchVariableRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchVariableRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchVariableRequest.VariableName) && r.ErrorMessage == "'Variable Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchVariableRequest.Value) && r.ErrorMessage == "'Value' must not be empty.");
        }
    }
}