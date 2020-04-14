using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class UpdateBatchRequestValidator : AbstractValidator<UpdateBatchRequest>
    {
        public UpdateBatchRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
        }
    }
}