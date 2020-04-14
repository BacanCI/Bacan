using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class CreateBatchOptionRequestValidator : AbstractValidator<CreateBatchOptionRequest>
    {
        public CreateBatchOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.OptionName).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}