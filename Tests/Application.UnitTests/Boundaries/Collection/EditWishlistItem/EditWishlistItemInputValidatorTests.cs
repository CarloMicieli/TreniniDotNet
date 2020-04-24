using FluentValidation.TestHelper;
using System;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem
{
    public class EditWishlistItemInputValidatorTests
    {
        private EditWishlistItemInputValidator Validator { get; }

        public EditWishlistItemInputValidatorTests()
        {
            Validator = new EditWishlistItemInputValidator();
        }

        [Fact]
        public void EditWishlistItemInput_ShouldSucceedValidation()
        {
            var input = CollectionInputs.EditWishlistItem.With(
                Id: Guid.NewGuid(),
                ItemId: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenItIsEmpty()
        {
            var input = CollectionInputs.EditWishlistItem.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = CollectionInputs.EditWishlistItem.With(Price: -1M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = CollectionInputs.EditWishlistItem.With(Price: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }


        [Fact]
        public void EditWishlistItem_ShouldFailValidation_WhenPriorityIsInvalid()
        {
            var input = CollectionInputs.EditWishlistItem.With(
                Priority: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Priority);
        }

        [Fact]
        public void EditWishlistItem_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = CollectionInputs.EditWishlistItem.With(
                Notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }
    }
}
