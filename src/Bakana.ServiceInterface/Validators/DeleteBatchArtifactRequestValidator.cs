using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class DeleteBatchArtifactRequestValidator : AbstractValidator<DeleteBatchArtifactRequest>
    {
        public DeleteBatchArtifactRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.ArtifactName).NotEmpty();
        }
    }
}