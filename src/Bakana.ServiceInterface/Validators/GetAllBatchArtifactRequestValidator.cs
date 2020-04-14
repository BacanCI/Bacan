using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetAllBatchArtifactRequestValidator : AbstractValidator<GetAllBatchArtifactRequest>
    {
        public GetAllBatchArtifactRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
        }
    }
}