using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class DeleteBatchVariableRequestValidator : AbstractValidator<DeleteBatchVariableRequest>
    {
        public DeleteBatchVariableRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.VariableName).NotEmpty();
        }
    }
}