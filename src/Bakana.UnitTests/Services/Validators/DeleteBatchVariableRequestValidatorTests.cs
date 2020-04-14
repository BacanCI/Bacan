using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class DeleteBatchVariableRequestValidatorTests
    {
        private DeleteBatchVariableRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DeleteBatchVariableRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new DeleteBatchVariableRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(DeleteBatchVariableRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(DeleteBatchVariableRequest.VariableName) && r.ErrorMessage == "'Variable Name' must not be empty.");
        }
    }
}