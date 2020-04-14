using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class DeleteBatchOptionRequestValidator : AbstractValidator<DeleteBatchOptionRequest>
    {
        public DeleteBatchOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.OptionName).NotEmpty();
        }
    }
}