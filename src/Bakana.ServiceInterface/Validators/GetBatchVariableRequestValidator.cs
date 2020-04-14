using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetBatchVariableRequestValidator : AbstractValidator<GetBatchVariableRequest>
    {
        public GetBatchVariableRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.VariableName).NotEmpty();
        }
    }
}