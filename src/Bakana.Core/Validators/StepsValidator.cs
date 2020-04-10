using System.Collections.Generic;
using System.Linq;
using Bakana.Core.Entities;
using FluentValidation;

namespace Bakana.Core.Validators
{
    public class StepsValidator : AbstractValidator<IList<Step>>
    {
        public StepsValidator()
        {
            RuleFor(steps => steps)
                .Custom((steps, context) =>
                {
                    if (steps.All(s => s.Dependencies.Length > 0))
                    {
                        context.AddFailure("All the steps have dependencies. None can be executed");
                    }
                    //foreach (var step in steps)
                    //{
                    //    var dependentSteps = GetDependencies(step, steps.ToList());
                    //    if (dependentSteps.Contains(step.Name))
                    //    {
                    //        context.AddFailure(nameof(Step.Dependencies), "Step has cyclic dependency");
                    //    }
                    //}
                });
        }

        //private IList<string> GetDependencies(Step step, List<Step> steps)
        //{
        //    var dependencies = step.Dependencies;

        //    if(dependencies == null || dependencies.Length == 0)
        //        return new List<string>();

        //    var dependentSteps = steps.Where(s => dependencies.Contains(s.Name));

        //    var indirectDependencies = new List<string>();
        //    foreach (var dependentStep in dependentSteps)
        //    {
        //        indirectDependencies.AddRange(GetDependencies(dependentStep, steps));
        //    }

        //    return indirectDependencies;
        //}
    }
}
