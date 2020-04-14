using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class UpdateBatchOptionRequestValidator : AbstractValidator<UpdateBatchOptionRequest>
    {
        public UpdateBatchOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.OptionName).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}