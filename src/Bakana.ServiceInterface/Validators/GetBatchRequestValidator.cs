using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetBatchRequestValidator : AbstractValidator<GetBatchRequest>
    {
        public GetBatchRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
        }
    }
}