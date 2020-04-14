using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class CreateBatchArtifactOptionRequestValidatorTests
    {
        private CreateBatchArtifactOptionRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CreateBatchArtifactOptionRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new CreateBatchArtifactOptionRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchArtifactOptionRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchArtifactOptionRequest.ArtifactName) && r.ErrorMessage == "'Artifact Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchArtifactOptionRequest.OptionName) && r.ErrorMessage == "'Option Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchArtifactOptionRequest.Value) && r.ErrorMessage == "'Value' must not be empty.");
        }
    }
}
