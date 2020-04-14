using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class UpdateBatchArtifactOptionRequestValidatorTests
    {
        private UpdateBatchArtifactOptionRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateBatchArtifactOptionRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new UpdateBatchArtifactOptionRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchArtifactOptionRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchArtifactOptionRequest.ArtifactName) && r.ErrorMessage == "'Artifact Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchArtifactOptionRequest.OptionName) && r.ErrorMessage == "'Option Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchArtifactOptionRequest.Value) && r.ErrorMessage == "'Value' must not be empty.");
        }
    }
}