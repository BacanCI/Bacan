using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class GetBatchArtifactOptionRequestValidatorTests
    {
        private GetBatchArtifactOptionRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetBatchArtifactOptionRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new GetBatchArtifactOptionRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(GetBatchArtifactOptionRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(GetBatchArtifactOptionRequest.ArtifactName) && r.ErrorMessage == "'Artifact Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(GetBatchArtifactOptionRequest.OptionName) && r.ErrorMessage == "'Option Name' must not be empty.");
        }
    }
}