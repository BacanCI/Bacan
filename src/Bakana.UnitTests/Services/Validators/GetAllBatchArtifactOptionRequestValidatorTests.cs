using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class GetAllBatchArtifactOptionRequestValidatorTests
    {
        private GetAllBatchArtifactOptionRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetAllBatchArtifactOptionRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new GetAllBatchArtifactOptionRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(GetAllBatchArtifactOptionRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(GetAllBatchArtifactOptionRequest.ArtifactName) && r.ErrorMessage == "'Artifact Name' must not be empty.");
        }
    }
}