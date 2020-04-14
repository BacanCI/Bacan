using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class CreateBatchArtifactOptionRequestValidator : AbstractValidator<CreateBatchArtifactOptionRequest>
    {
        public CreateBatchArtifactOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.ArtifactName).NotEmpty();
            RuleFor(x => x.OptionName).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}
