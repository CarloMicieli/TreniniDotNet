using FluentValidation.TestHelper;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
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
            var input = NewCreateScaleInput.With(
                Name: "H0",
                Ratio: 87M,
                Gauge: NewScaleGaugeInput.With(
                    Millimeters: 16.5M,
                    TrackGauge: TrackGauge.Standard.ToString()),
                Description: "my notes");

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = NewCreateScaleInput.Empty;

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsEmpty()
        {
            var input = NewCreateScaleInput.With(Name: "   ");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsTooLong()
        {
            var input = NewCreateScaleInput.With(Name: "01234567890");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenRatioIsNegative()
        {
            var input = NewCreateScaleInput.With(Ratio: -1M);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Ratio);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenRatioIsZero()
        {
            var input = NewCreateScaleInput.With(Ratio: 0M);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Ratio);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenGaugeIsNegative()
        {
            var input = NewCreateScaleInput.With(Gauge: NewScaleGaugeInput.With(Millimeters: -10M));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.Millimeters);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenGaugeIsZero()
        {
            var input = NewCreateScaleInput.With(Gauge: NewScaleGaugeInput.With(Millimeters: 0M));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.Millimeters);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenTrackGaugeIsInvalid()
        {
            var input = NewCreateScaleInput.With(Gauge: NewScaleGaugeInput.With(TrackGauge: "invalid"));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.TrackGauge);
        }
    }
}
