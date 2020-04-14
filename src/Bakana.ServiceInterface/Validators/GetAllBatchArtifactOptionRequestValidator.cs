using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetAllBatchArtifactOptionRequestValidator : AbstractValidator<GetAllBatchArtifactOptionRequest>
    {
        public GetAllBatchArtifactOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.ArtifactName).NotEmpty();
        }
    }
}