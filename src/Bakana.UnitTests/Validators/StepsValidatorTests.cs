using System;
using System.Collections.Generic;
using Bakana.Core.Entities;
using Bakana.Core.Validators;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace Bakana.UnitTests.Validators
{
    [TestFixture]
    public class StepsValidatorTests
    {
        private StepsValidator _stepsValidator;

        [SetUp]
        public void SetUp()
        {
            var fn = Substitute.For<Func<StepValidatorContext, IValidator<Step>>>();
            fn(Arg.Any<StepValidatorContext>()).Validate(Arg.Any<ValidationContext>()).Returns(new ValidationResult());
            _stepsValidator = new StepsValidator(fn);
        }

        [Test]
        public void Should_Have_Error_When_All_Steps_Have_Dependencies()
        {
            var steps = new List<Step>
            {
                new Step
                {
                    Name = "Step1",
                    Dependencies = new[] {"Step2"}
                },
                new Step
                {
                    Name = "Step2",
                    Dependencies = new []{"Step3"}
                },
                new Step
                {
                    Name = "Step3",
                    Dependencies = new []{"Step1"}
                }

            };

            var result = _stepsValidator.TestValidate(steps);

            result.Errors.Should().Contain(e => e.ErrorMessage == "All the steps have dependencies. None can be executed");
        }

        [Test]
        public void Should_Have_Error_When_Steps_Have_Cyclic_Dependencies()
        {
            var steps = new List<Step>
            {
                new Step
                {
                    Name = "Step1"
                },
                new Step
                {
                    Name = "Step2",
                    Dependencies = new []{"Step3"}
                },
                new Step
                {
                    Name = "Step3",
                    Dependencies = new []{"Step4"}
                },
                new Step
                {
                    Name = "Step4",
                    Dependencies = new []{"Step2"}
                }

            };

            var result = _stepsValidator.TestValidate(steps);

            result.Errors.Should().Contain(e => e.ErrorMessage == "Steps cannot have cyclic dependencies");
        }

        [Test]
        public void Should_Not_Have_Errors_When_All_Steps_Can_Be_Executed()
        {
            var steps = new List<Step>
            {
                new Step
                {
                    Name = "Step1",
                    Dependencies = new []{"Step4"}
                },
                new Step
                {
                    Name = "Step2",
                    Dependencies = new []{"Step3"}
                },
                new Step
                {
                    Name = "Step3",
                    Dependencies = new []{"Step4"}
                },
                new Step
                {
                    Name = "Step4"
                }

            };

            var result = _stepsValidator.TestValidate(steps);

            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
