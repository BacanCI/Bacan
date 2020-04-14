using System.Collections.Generic;
using Bakana.Core.Entities;
using Bakana.Core.Validators;
using FluentValidation;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace Bakana.UnitTests.Validators
{
    [TestFixture]
    public class BatchValidatorTests
    {
        private BatchValidator _batchValidator;

        [SetUp]
        public void SetUp()
        {
            var stepsValidator = Substitute.For<IValidator<IList<Step>>>();
            _batchValidator = new BatchValidator(stepsValidator);
        }

        [Test]
        public void Should_Have_Error_When_Id_Is_Null()
        {
            _batchValidator.ShouldHaveValidationErrorFor(s => s.Id, null as string)
                .WithErrorMessage("Batch id is not specified");
        }

        [Test]
        public void Should_Have_Error_When_No_Steps_Are_Supplied()
        {
            _batchValidator.ShouldHaveValidationErrorFor(s => s.Steps, new List<Step>())
                .WithErrorMessage("A batch must have at least one step");
        }
    }
}
