using System;
using FluentValidation.TestHelper;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
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
            var input = NewCreateRailwayInput.With(
                Name: "DB",
                CompanyName: "Die Bahn",
                Country: "DE",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "Inactive",
                    OperatingSince: DateTime.Now.AddDays(-1),
                    OperatingUntil: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = NewCreateRailwayInput.With(
                Name: null,
                CompanyName: "Die Bahn",
                Country: "DE",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "inactive",
                    OperatingSince: DateTime.Now.AddDays(-1),
                    OperatingUntil: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenNameIsEmpty()
        {
            var input = NewCreateRailwayInput.With(
                Name: "   ",
                CompanyName: "Die Bahn",
                Country: "DE",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "inactive",
                    OperatingSince: DateTime.Now.AddDays(-1),
                    OperatingUntil: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenCountryIsNotValid()
        {
            var input = NewCreateRailwayInput.With(
                Name: "DB",
                CompanyName: "Die Bahn",
                Country: "YY",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "inactive",
                    OperatingSince: DateTime.Now.AddDays(-1),
                    OperatingUntil: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenOperatingUntilIsBeforeOperatingSince()
        {
            var input = NewCreateRailwayInput.With(
                Name: "DB",
                CompanyName: "Die Bahn",
                Country: "YY",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "inactive",
                    OperatingSince: DateTime.Now.AddDays(1),
                    OperatingUntil: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PeriodOfActivity.OperatingUntil);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenStatusIsInvalid()
        {
            var input = NewCreateRailwayInput.With(
                Name: "DB",
                CompanyName: "Die Bahn",
                Country: "YY",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "not valid",
                    OperatingSince: DateTime.Now.AddDays(-1),
                    OperatingUntil: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PeriodOfActivity.Status);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenActiveRailwayHaveTerminateDate()
        {
            var input = NewCreateRailwayInput.With(
                Name: "DB",
                CompanyName: "Die Bahn",
                Country: "YY",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "active",
                    OperatingSince: DateTime.Now.AddDays(-1),
                    OperatingUntil: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PeriodOfActivity.Status);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenRailwayTotalLengthIsNegative()
        {
            var input = NewCreateRailwayInput.With(
                Name: "DB",
                Country: "DE",
                TotalLength: NewTotalRailwayLengthInput.With(
                    Kilometers: -10M,
                    Miles: -10M),
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "active",
                    OperatingSince: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.TotalLength.Kilometers);
            result.ShouldHaveValidationErrorFor(x => x.TotalLength.Miles);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenRailwayGaugeIsNegative()
        {
            var input = NewCreateRailwayInput.With(
                Name: "DB",
                Country: "DE",
                Gauge: NewRailwayGaugeInput.With(
                    Inches: -10M,
                    Millimeters: -10M),
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "active",
                    OperatingSince: DateTime.Now));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.Millimeters);
            result.ShouldHaveValidationErrorFor(x => x.Gauge.Inches);
        }
    }
}