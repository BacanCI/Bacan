using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class CreateBatchArtifactRequestValidator : AbstractValidator<CreateBatchArtifactRequest>
    {
        public CreateBatchArtifactRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.ArtifactName).NotEmpty();
            RuleFor(x => x.FileName).NotEmpty();
        }
    }
}