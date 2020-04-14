using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class CreateBatchArtifactRequestValidatorTests
    {
        private CreateBatchArtifactRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CreateBatchArtifactRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new CreateBatchArtifactRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchArtifactRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchArtifactRequest.ArtifactName) && r.ErrorMessage == "'Artifact Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(CreateBatchArtifactRequest.FileName) && r.ErrorMessage == "'File Name' must not be empty.");
        }
    }
}