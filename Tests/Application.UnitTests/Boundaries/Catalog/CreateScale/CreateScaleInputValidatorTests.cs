using FluentValidation.TestHelper;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;
using TreniniDotNet.Domain.Catalog.ValueObjects;

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
            var input = NewScaleInput.With(
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
            var input = NewScaleInput.Empty;

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsEmpty()
        {
            var input = NewScaleInput.With(Name: "   ");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenNameIsTooLong()
        {
            var input = NewScaleInput.With(Name: "01234567890");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenRatioIsNegative()
        {
            var input = NewScaleInput.With(Ratio: -1M);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Ratio);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenRatioIsZero()
        {
            var input = NewScaleInput.With(Ratio: 0M);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Ratio);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenGaugeIsNegative()
        {
            var input = NewScaleInput.With(Gauge: NewScaleGaugeInput.With(Millimeters: -10M));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.Millimeters);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenGaugeIsZero()
        {
            var input = NewScaleInput.With(Gauge: NewScaleGaugeInput.With(Millimeters: 0M));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.Millimeters);
        }

        [Fact]
        public void CreateScaleInputValidator_ShouldHaveError_WhenTrackGaugeIsInvalid()
        {
            var input = NewScaleInput.With(Gauge: NewScaleGaugeInput.With(TrackGauge: "invalid"));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Gauge.TrackGauge);
        }
    }
}
