using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetAllBatchVariableRequestValidator : AbstractValidator<GetAllBatchVariableRequest>
    {
        public GetAllBatchVariableRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
        }
    }
}