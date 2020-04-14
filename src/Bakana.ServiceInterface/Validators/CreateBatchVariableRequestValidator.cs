using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class CreateBatchVariableRequestValidator : AbstractValidator<CreateBatchVariableRequest>
    {
        public CreateBatchVariableRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.VariableName).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}