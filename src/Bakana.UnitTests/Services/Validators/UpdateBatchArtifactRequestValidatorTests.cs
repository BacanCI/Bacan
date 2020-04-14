using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class UpdateBatchArtifactRequestValidatorTests
    {
        private UpdateBatchArtifactRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateBatchArtifactRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new UpdateBatchArtifactRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchArtifactRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchArtifactRequest.ArtifactName) && r.ErrorMessage == "'Artifact Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(UpdateBatchArtifactRequest.FileName) && r.ErrorMessage == "'File Name' must not be empty.");
        }
    }
}