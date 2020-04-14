using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class DeleteBatchRequestValidator : AbstractValidator<DeleteBatchRequest>
    {
        public DeleteBatchRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
        }
    }
}