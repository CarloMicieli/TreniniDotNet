using FluentValidation.TestHelper;
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
            var input = CollectingInputs.AddItemToCollection.With(
                owner: "George",
                catalogItem: "123456",
                price: 199M);

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenEmpty()
        {
            var input = CollectingInputs.AddItemToCollection.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = CollectingInputs.AddItemToCollection.With(price: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = CollectingInputs.AddItemToCollection.With(price: -100M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenItemNumberIsTooLong()
        {
            var input = CollectingInputs.AddItemToCollection.With(
                catalogItem: RandomString.WithLengthOf(61));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenConditionIsInvalid()
        {
            var input = CollectingInputs.AddItemToCollection.With(
                condition: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Condition);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenNotesIsTooLong()
        {
            var input = CollectingInputs.AddItemToCollection.With(
                notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenShopIsTooLong()
        {
            var input = CollectingInputs.AddItemToCollection.With(
                shop: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Shop);
        }
    }
}
