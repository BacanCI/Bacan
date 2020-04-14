using Bakana.ServiceInterface.Validators;
using Bakana.ServiceModels.Batches;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests.Services.Validators
{
    [TestFixture]
    public class DeleteBatchArtifactOptionRequestValidatorTests
    {
        private DeleteBatchArtifactOptionRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DeleteBatchArtifactOptionRequestValidator();
        }

        [Test]
        public void Should_Have_Errors_When_Required_Fields_Are_Empty()
        {
            var result = _sut.Validate(new DeleteBatchArtifactOptionRequest());
            result.Errors.Should().Contain(r => r.PropertyName == nameof(DeleteBatchArtifactOptionRequest.BatchId) && r.ErrorMessage == "'Batch Id' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(DeleteBatchArtifactOptionRequest.ArtifactName) && r.ErrorMessage == "'Artifact Name' must not be empty.");
            result.Errors.Should().Contain(r => r.PropertyName == nameof(DeleteBatchArtifactOptionRequest.OptionName) && r.ErrorMessage == "'Option Name' must not be empty.");
        }
    }
}