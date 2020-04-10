using System.Collections.Generic;
using Bakana.Core.Entities;
using FluentValidation;

namespace Bakana.Core.Validators
{
    public class StepValidator : AbstractValidator<Step>
    {
        public StepValidator(StepValidatorContext validatorContext)
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage("Step Name must be specified");

            RuleFor(s => s.Commands)
                .Must(c => c != null && c.Count > 0)
                .WithMessage("A step must have at least one command");

            RuleFor(s => s.Dependencies)
                .Custom((dependencies, context) =>
                {
                    if (dependencies != null && dependencies.Length > 0)
                    {
                        foreach (var dependency in dependencies)
                        {
                            if (!validatorContext.StepIds.Contains(dependency))
                            {
                                context.AddFailure($"Unknown dependency :{dependency}");
                            }
                        }
                    }
                });
        }
    }

    public class StepValidatorContext
    {
        public List<string> StepIds { get; set; } = new List<string>();
    }
}
