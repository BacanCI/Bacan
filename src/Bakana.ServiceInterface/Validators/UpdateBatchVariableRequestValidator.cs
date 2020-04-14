using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class UpdateBatchVariableRequestValidator : AbstractValidator<UpdateBatchVariableRequest>
    {
        public UpdateBatchVariableRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.VariableName).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}