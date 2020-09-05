using FluentValidation.TestHelper;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public class AddItemToCollectionInputValidatorTests
    {
        private AddItemToCollectionInputValidator Validator { get; }

        public AddItemToCollectionInputValidatorTests()
        {
            Validator = new AddItemToCollectionInputValidator();
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldSucceedValidation()
        {
            var input = NewAddItemToCollectionInput.With(
                owner: "George",
                catalogItem: "123456",
                price: NewPriceInput.With(450M, "EUR"));

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewAddItemToCollectionInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = NewAddItemToCollectionInput.With(price: NewPriceInput.Empty);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price.Value);
            result.ShouldHaveValidationErrorFor(x => x.Price.Currency);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = NewAddItemToCollectionInput.With(price: NewPriceInput.With(-100M));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price.Value);
            result.ShouldHaveValidationErrorFor(x => x.Price.Currency);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenItemNumberIsTooLong()
        {
            var input = NewAddItemToCollectionInput.With(
                catalogItem: RandomString.WithLengthOf(61));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenConditionIsInvalid()
        {
            var input = NewAddItemToCollectionInput.With(
                condition: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Condition);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenNotesIsTooLong()
        {
            var input = NewAddItemToCollectionInput.With(
                notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenShopIsTooLong()
        {
            var input = NewAddItemToCollectionInput.With(
                shop: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Shop);
        }
    }
}