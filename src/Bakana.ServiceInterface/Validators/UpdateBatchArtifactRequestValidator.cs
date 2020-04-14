using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class UpdateBatchArtifactRequestValidator : AbstractValidator<UpdateBatchArtifactRequest>
    {
        public UpdateBatchArtifactRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.ArtifactName).NotEmpty();
            RuleFor(x => x.FileName).NotEmpty();
        }
    }
}