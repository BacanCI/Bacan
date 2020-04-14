using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetBatchOptionRequestValidator : AbstractValidator<GetBatchOptionRequest>
    {
        public GetBatchOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
            RuleFor(x => x.OptionName).NotEmpty();
        }
    }
}