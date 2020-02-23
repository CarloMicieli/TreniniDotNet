using FluentValidation.TestHelper;
using System;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateRailway
{
    public class CreateRailwayInputValidatorTests
    {
        private readonly CreateRailwayInputValidator validator;

        public CreateRailwayInputValidatorTests()
        {
            validator = new CreateRailwayInputValidator();
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveNoError_WhenInputObjectIsValid()
        {
            var input = new CreateRailwayInput("DB", "Die Bahn", "DE", "inactive", DateTime.Now.AddDays(-1), DateTime.Now);

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = new CreateRailwayInput(null, "Die Bahn", "DE", "inactive", DateTime.Now.AddDays(-1), DateTime.Now);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenNameIsEmpty()
        {
            var input = new CreateRailwayInput("   ", "Die Bahn", "DE", "inactive", DateTime.Now.AddDays(-1), DateTime.Now);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenCountryIsNotValid()
        {
            var input = new CreateRailwayInput(null, "Die Bahn", "YY", "inactive", DateTime.Now.AddDays(-1), DateTime.Now);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenOperatingUntilIsBeforeOperatingSince()
        {
            var input = new CreateRailwayInput("DB", "Die Bahn", "DE", "inactive", DateTime.Now.AddDays(1), DateTime.Now);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.OperatingUntil);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenStatusIsInvalid()
        {
            var input = new CreateRailwayInput("DB", "Die Bahn", "DE", "not valid", DateTime.Now.AddDays(-1), DateTime.Now);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Status);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenActiveRailwayHaveTerminateDate()
        {
            var input = new CreateRailwayInput(null, "Die Bahn", "DE", "active", DateTime.Now.AddDays(-1), DateTime.Now);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}
