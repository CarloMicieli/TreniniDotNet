using System;
using FluentValidation.TestHelper;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public class AddItemToWishlistInputValidatorTests
    {
        private AddItemToWishlistInputValidator Validator { get; }

        public AddItemToWishlistInputValidatorTests()
        {
            Validator = new AddItemToWishlistInputValidator();
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldSucceedValidation()
        {
            var input = CollectingInputs.AddItemToWishlist.With(
                id: Guid.NewGuid(),
                catalogItem: "acme-123456");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenItIsEmpty()
        {
            var input = CollectingInputs.AddItemToWishlist.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = CollectingInputs.AddItemToWishlist.With(price: -1M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = CollectingInputs.AddItemToWishlist.With(price: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenItemNumberIsTooLong()
        {
            var input = CollectingInputs.AddItemToWishlist.With(
                catalogItem: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriorityIsInvalid()
        {
            var input = CollectingInputs.AddItemToWishlist.With(
                priority: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Priority);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = CollectingInputs.AddItemToWishlist.With(
                notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }
    }
}
