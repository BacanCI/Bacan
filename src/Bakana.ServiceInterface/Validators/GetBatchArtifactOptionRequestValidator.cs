using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetBatchArtifactOptionRequestValidator : AbstractValidator<GetBatchArtifactOptionRequest>
    {
        public GetBatchArtifactOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.ArtifactName).NotEmpty();
            RuleFor(x => x.OptionName).NotEmpty();
        }
    }
}