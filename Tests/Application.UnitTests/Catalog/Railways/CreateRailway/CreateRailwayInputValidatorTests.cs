using System;
using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public class CreateRailwayInputValidatorTests
    {
        private CreateRailwayInputValidator Validator { get; }

        public CreateRailwayInputValidatorTests()
        {
            Validator = new CreateRailwayInputValidator();
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveNoError_WhenInputObjectIsValid()
        {
            var input = NewCreateRailwayInput.With(
                name: "DB",
                companyName: "Die Bahn",
                country: "DE",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "Inactive",
                    operatingSince: DateTime.Now.AddDays(-1),
                    operatingUntil: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = NewCreateRailwayInput.With(
                name: null,
                companyName: "Die Bahn",
                country: "DE",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "inactive",
                    operatingSince: DateTime.Now.AddDays(-1),
                    operatingUntil: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenNameIsEmpty()
        {
            var input = NewCreateRailwayInput.With(
                name: "   ",
                companyName: "Die Bahn",
                country: "DE",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "inactive",
                    operatingSince: DateTime.Now.AddDays(-1),
                    operatingUntil: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenCountryIsNotValid()
        {
            var input = NewCreateRailwayInput.With(
                name: "DB",
                companyName: "Die Bahn",
                country: "YY",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "inactive",
                    operatingSince: DateTime.Now.AddDays(-1),
                    operatingUntil: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenOperatingUntilIsBeforeOperatingSince()
        {
            var input = NewCreateRailwayInput.With(
                name: "DB",
                companyName: "Die Bahn",
                country: "YY",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "inactive",
                    operatingSince: DateTime.Now.AddDays(1),
                    operatingUntil: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PeriodOfActivity.OperatingUntil);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenStatusIsInvalid()
        {
            var input = NewCreateRailwayInput.With(
                name: "DB",
                companyName: "Die Bahn",
                country: "YY",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "not valid",
                    operatingSince: DateTime.Now.AddDays(-1),
                    operatingUntil: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PeriodOfActivity.Status);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenActiveRailwayHaveTerminateDate()
        {
            var input = NewCreateRailwayInput.With(
                name: "DB",
                companyName: "Die Bahn",
                country: "YY",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "active",
                    operatingSince: DateTime.Now.AddDays(-1),
                    operatingUntil: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PeriodOfActivity.Status);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenRailwayTotalLengthIsNegative()
        {
            var input = NewCreateRailwayInput.With(
                name: "DB",
                country: "DE",
                totalLength: NewTotalRailwayLengthInput.With(
                    kilometers: -10M,
                    miles: -10M),
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "active",
                    operatingSince: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.TotalLength.Kilometers);
            result.ShouldHaveValidationErrorFor(x => x.TotalLength.Miles);
        }

        [Fact]
        public void CreateRailwayInputValidator_ShouldHaveError_WhenRailwayGaugeIsNegative()
        {
            var input = NewCreateRailwayInput.With(
                name: "DB",
                country: "DE",
                gauge: NewRailwayGaugeInput.With(
                    inches: -10M,
                    millimeters: -10M),
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "active",
                    operatingSince: DateTime.Now));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.Millimeters);
            result.ShouldHaveValidationErrorFor(x => x.Gauge.Inches);
        }
    }
}
