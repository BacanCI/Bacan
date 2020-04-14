using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class CreateBatchVariableRequestValidatorTests
    {
        private CreateBatchVariableRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CreateBatchVariableRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new CreateBatchVariableRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchVariableRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchVariableRequest.VariableName) && r.ErrorMessage == "'Variable Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchVariableRequest.Value) && r.ErrorMessage == "'Value' must not be empty.");
        }
    }
}