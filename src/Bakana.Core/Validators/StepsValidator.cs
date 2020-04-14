using System;
using System.Collections.Generic;
using System.Linq;
using Bakana.Core.Entities;
using FluentValidation;

namespace Bakana.Core.Validators
{
    public class StepsValidator : AbstractValidator<IList<Step>>
    {
        public StepsValidator(Func<StepValidatorContext, IValidator<Step>> stepValidatorFn)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleForEach(steps => steps)
                .SetValidator(c =>
                {
                    var context = new StepValidatorContext
                    {
                        StepIds = c.Select(x => x.Name).ToList()
                    };
                    return stepValidatorFn(context);
                });

            RuleFor(steps => steps)
                .Custom((steps, context) =>
                {
                    if (steps.All(s => s.Dependencies != null && s.Dependencies.Length > 0))
                    {
                        context.AddFailure("All the steps have dependencies. None can be executed");
                        return;
                    }

                    var stepsStatus = steps.ToDictionary(s => s.Name, s => false);

                    var independentSteps =
                        steps.Where(s => s.Dependencies == null || s.Dependencies.Length == 0).ToList();

                    foreach (var independentStep in independentSteps)
                    {
                        stepsStatus[independentStep.Name] = true;
                    }

                    while (stepsStatus.Any(s => s.Value == false))
                    {
                        var pendingSteps = stepsStatus.Count(s => s.Value == false);

                        var dependentSteps = steps.Where(s => s.Dependencies != null && s.Dependencies.Length > 0);
                        foreach (var dependentStep in dependentSteps)
                        {
                            var dependencies = dependentStep.Dependencies;

                            if (dependencies.All(d => stepsStatus[d]))
                            {
                                stepsStatus[dependentStep.Name] = true;
                            }
                        }

                        if (pendingSteps == stepsStatus.Count(s => s.Value == false))
                        {
                            // deadlock
                            break;
                        }
                    }

                    if (stepsStatus.Any(s => s.Value == false))
                    {
                        context.AddFailure("Steps cannot have cyclic dependencies");
                    }
                });
        }

       
    }
}
