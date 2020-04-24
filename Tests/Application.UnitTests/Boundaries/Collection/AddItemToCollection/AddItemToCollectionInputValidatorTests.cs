using FluentValidation.TestHelper;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection
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
            var input = CollectionInputs.AddItemToCollection.With(
                Owner: "George",
                CatalogItem: "123456",
                Price: 199M);

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenEmpty()
        {
            var input = CollectionInputs.AddItemToCollection.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = CollectionInputs.AddItemToCollection.With(Price: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = CollectionInputs.AddItemToCollection.With(Price: -100M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenItemNumberIsTooLong()
        {
            var input = CollectionInputs.AddItemToCollection.With(
                CatalogItem: RandomString.WithLengthOf(61));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenConditionIsInvalid()
        {
            var input = CollectionInputs.AddItemToCollection.With(
                Condition: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Condition);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenNotesIsTooLong()
        {
            var input = CollectionInputs.AddItemToCollection.With(
                Notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }

        [Fact]
        public void AddItemToCollectionInput_ShouldFailValidation_WhenShopIsTooLong()
        {
            var input = CollectionInputs.AddItemToCollection.With(
                Shop: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Shop);
        }
    }
}
