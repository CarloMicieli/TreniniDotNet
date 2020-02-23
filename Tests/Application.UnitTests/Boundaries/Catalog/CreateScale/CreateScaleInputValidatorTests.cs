using FluentValidation.TestHelper;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public class CreateScaleInputValidatorTests
    {
        private readonly CreateScaleInputValidator validator;

        public CreateScaleInputValidatorTests()
        {
            validator = new CreateScaleInputValidator();
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveNoError_WhenInputObjectIsValid()
        {
            var input = new CreateScaleInput("H0", 87M, 16.5M, TrackGauge.Standard.ToString(), "my notes");

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = new CreateScaleInput(null, null, null, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsEmpty()
        {
            var input = new CreateScaleInput("   ", null, null, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsTooLong()
        {
            var input = new CreateScaleInput("01234567890", null, null, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenRatioIsNegative()
        {
            var input = new CreateScaleInput(null, -1M, null, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Ratio);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenRatioIsZero()
        {
            var input = new CreateScaleInput(null, 0M, null, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Ratio);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenGaugeIsNegative()
        {
            var input = new CreateScaleInput(null, null, -1M, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenGaugeIsZero()
        {
            var input = new CreateScaleInput(null, null, 0M, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenTrackGaugeIsInvalid()
        {
            var input = new CreateScaleInput(null, null, null, "not valid", null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.TrackGauge);
        }
    }
}
