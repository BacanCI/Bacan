using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class DeleteBatchArtifactOptionRequestValidator : AbstractValidator<DeleteBatchArtifactOptionRequest>
    {
        public DeleteBatchArtifactOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.ArtifactName).NotEmpty();
            RuleFor(x => x.OptionName).NotEmpty();
        }
    }
}