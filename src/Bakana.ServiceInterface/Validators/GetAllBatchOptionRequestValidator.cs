using Bakana.ServiceModels.Batches;
using ServiceStack.FluentValidation;

namespace Bakana.ServiceInterface.Validators
{
    public class GetAllBatchOptionRequestValidator : AbstractValidator<GetAllBatchOptionRequest>
    {
        public GetAllBatchOptionRequestValidator()
        {
            RuleFor(x => x.BatchId).NotEmpty();
        }
    }
}