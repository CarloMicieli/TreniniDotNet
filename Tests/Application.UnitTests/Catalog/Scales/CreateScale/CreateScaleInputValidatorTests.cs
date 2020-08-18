using FluentValidation.TestHelper;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
{
    public class CreateScaleInputValidatorTests
    {
        private CreateScaleInputValidator Validator { get; }

        public CreateScaleInputValidatorTests()
        {
            Validator = new CreateScaleInputValidator();
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveNoError_WhenInputObjectIsValid()
        {
            var input = NewCreateScaleInput.With(
                name: "H0",
                ratio: 87M,
                scaleGauge: NewScaleGaugeInput.With(
                    millimeters: 16.5M,
                    trackGauge: TrackGauge.Standard.ToString()),
                description: "my notes");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = NewCreateScaleInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsEmpty()
        {
            var input = NewCreateScaleInput.With(name: "   ");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsTooLong()
        {
            var input = NewCreateScaleInput.With(name: "01234567890");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenRatioIsNegative()
        {
            var input = NewCreateScaleInput.With(ratio: -1M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Ratio);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenRatioIsZero()
        {
            var input = NewCreateScaleInput.With(ratio: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Ratio);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenGaugeIsNegative()
        {
            var input = NewCreateScaleInput.With(scaleGauge: NewScaleGaugeInput.With(millimeters: -10M));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.Millimeters);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenGaugeIsZero()
        {
            var input = NewCreateScaleInput.With(scaleGauge: NewScaleGaugeInput.With(millimeters: 0M));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.Millimeters);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenTrackGaugeIsInvalid()
        {
            var input = NewCreateScaleInput.With(scaleGauge: NewScaleGaugeInput.With(trackGauge: "invalid"));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.TrackGauge);
        }
    }
}
