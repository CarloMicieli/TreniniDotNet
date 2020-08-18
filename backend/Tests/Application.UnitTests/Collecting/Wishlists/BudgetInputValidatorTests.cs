using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public class BudgetInputValidatorTests
    {
        private BudgetInputValidator Validator { get; }

        public BudgetInputValidatorTests()
        {
            Validator = new BudgetInputValidator();
        }

        [Fact]
        public void BudgetInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewBudgetInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Value);
            result.ShouldHaveValidationErrorFor(x => x.Currency);
        }

        [Fact]
        public void BudgetInput_ShouldFailValidation_WhenValueIsNegative()
        {
            var input = NewBudgetInput.With(-250M, "EUR");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Value);
        }

        [Fact]
        public void BudgetInput_ShouldFailValidation_WhenCurrencyCodeIsInvalid()
        {
            var input = NewBudgetInput.With(250M, "---");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Currency);
        }

        [Fact]
        public void BudgetInput_ShouldSucceedValidation_WhenValid()
        {
            var input = NewBudgetInput.With(250M, "EUR");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
