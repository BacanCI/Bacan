using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class GetBatchVariableRequestValidatorTests
    {
        private GetBatchVariableRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetBatchVariableRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new GetBatchVariableRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(GetBatchVariableRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(GetBatchVariableRequest.VariableName) && r.ErrorMessage == "'Variable Name' must not be empty.");
        }
    }
}