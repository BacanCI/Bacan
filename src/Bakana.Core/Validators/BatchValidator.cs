using System.Collections.Generic;
using Bakana.Core.Entities;
using FluentValidation;

namespace Bakana.Core.Validators
{
    public class BatchValidator : AbstractValidator<Batch>
    {
        public BatchValidator(IValidator<IList<Step>> stepsValidator)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(b => b.Id)
                .NotEmpty()
                .WithMessage("Batch id is not specified");

            RuleFor(b => b.Steps)
                .Must(s => s != null && s.Count > 0)
                .WithMessage("A batch must have at least one step");

            RuleFor(b => b.Steps)
                .SetValidator(stepsValidator)
                .When(b => b.Steps != null && b.Steps.Count > 0);

        }
    }
}
