using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetBatchArtifactRequestValidator : AbstractValidator<GetBatchArtifactRequest>
    {
        public GetBatchArtifactRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.ArtifactName).NotEmpty();
        }
    }
}