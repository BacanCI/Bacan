using Bakana.Core.Entities;
using FluentValidation;

namespace Bakana.Core.Validators
{
    public class BatchValidator : AbstractValidator<Batch>
    {
        public BatchValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(b => b.Id)
                .NotEmpty()
                .WithMessage("Batch id is not specified");

            RuleFor(b => b.Steps)
                .Must(s => s != null && s.Count > 0)
                .WithMessage("A batch must have at least one step");

        }
    }
}
