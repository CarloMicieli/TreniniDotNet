using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shared
{
    public class PriceInputValidatorTests
    {
        private PriceInputValidator Validator { get; }

        public PriceInputValidatorTests()
        {
            Validator = new PriceInputValidator();
        }

        [Fact]
        public void PriceInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewPriceInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Value);
            result.ShouldHaveValidationErrorFor(x => x.Currency);
        }

        [Fact]
        public void PriceInput_ShouldFailValidation_WhenValueIsNegative()
        {
            var input = NewPriceInput.With(-250M, "EUR");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Value);
        }

        [Fact]
        public void PriceInput_ShouldFailValidation_WhenCurrencyCodeIsInvalid()
        {
            var input = NewPriceInput.With(250M, "---");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Currency);
        }

        [Fact]
        public void PriceInput_ShouldSucceedValidation_WhenValid()
        {
            var input = NewPriceInput.With(250M, "EUR");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
