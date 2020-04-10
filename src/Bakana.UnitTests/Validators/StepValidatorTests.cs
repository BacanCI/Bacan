using System.Collections.Generic;
using Bakana.Core.Entities;
using Bakana.Core.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Bakana.UnitTests.Validators
{
    [TestFixture]
    public class StepValidatorTests
    {
        private StepValidator _stepValidator;

        [SetUp]
        public void SetUp()
        {
            var validatorContext = new StepValidatorContext
            {
                StepIds = new List<string>
                {
                    "Step1",
                    "Step2"
                }
            };
            _stepValidator = new StepValidator(validatorContext);
        }

        [Test]
        public void Should_Have_Error_When_StepId_Is_Null()
        {
            _stepValidator.ShouldHaveValidationErrorFor(s => s.StepId, null as string)
                .WithErrorMessage("Step id must be specified");
        }

        [Test]
        public void Should_Have_Error_When_No_Commands_Are_Supplied()
        {
            var step = new Step();

            var result = _stepValidator.TestValidate(step);

            result.ShouldHaveValidationErrorFor(s => s.Commands)
                .WithErrorMessage("A step must have at least one command");
        }

        [Test]
        public void Should_Have_Error_When_Dependency_Is_Not_Valid()
        {
            var step = new Step
            {
                Dependencies = new []{"Step3"}
            };

            var result = _stepValidator.TestValidate(step);

            result.ShouldHaveValidationErrorFor(s => s.Dependencies)
                .WithErrorMessage("Unknown dependency :Step3");
        }
    }
}
