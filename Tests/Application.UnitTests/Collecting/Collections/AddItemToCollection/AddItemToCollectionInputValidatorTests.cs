using FluentValidation.TestHelper;
using TreniniDotNet.Application.TestInputs.Collecting;
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
                Owner: "George",
                CatalogItem: "123456",
                Price: 199M);

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
            var input = CollectingInputs.AddItemToCollection.With(Price: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = CollectingInputs.AddItemToCollection.With(Price: -100M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenItemNumberIsTooLong()
        {
            var input = CollectingInputs.AddItemToCollection.With(
                CatalogItem: RandomString.WithLengthOf(61));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenConditionIsInvalid()
        {
            var input = CollectingInputs.AddItemToCollection.With(
                Condition: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Condition);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenNotesIsTooLong()
        {
            var input = CollectingInputs.AddItemToCollection.With(
                Notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenShopIsTooLong()
        {
            var input = CollectingInputs.AddItemToCollection.With(
                Shop: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Shop);
        }
    }
}
