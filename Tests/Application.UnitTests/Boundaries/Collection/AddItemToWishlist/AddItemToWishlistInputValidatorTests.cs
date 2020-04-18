using FluentValidation.TestHelper;
using System;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist
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
            var input = CollectionInputs.AddItemToWishlist.With(
                Id: Guid.NewGuid(),
                CatalogItem: "acme-123456");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenItIsEmpty()
        {
            var input = CollectionInputs.AddItemToWishlist.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = CollectionInputs.AddItemToWishlist.With(Price: -1M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = CollectionInputs.AddItemToWishlist.With(Price: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenItemNumberIsTooLong()
        {
            var input = CollectionInputs.AddItemToWishlist.With(
                CatalogItem: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriorityIsInvalid()
        {
            var input = CollectionInputs.AddItemToWishlist.With(
                Priority: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Priority);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = CollectionInputs.AddItemToWishlist.With(
                Notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }
    }
}
