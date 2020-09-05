using System;
using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlist
{
    public class EditWishlistInputValidatorTests
    {
        private EditWishlistInputValidator Validator { get; }

        public EditWishlistInputValidatorTests()
        {
            Validator = new EditWishlistInputValidator();
        }

        [Fact]
        public void EditWishlistInput_ShouldSucceedValidation()
        {
            var input = NewEditWishlistInput.With(Guid.NewGuid(), "owner");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void EditWishlistInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewEditWishlistInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void EditWishlistInput_ShouldFailValidation_WhenVisibilityIsNotValid()
        {
            var input = NewEditWishlistInput.With(visibility: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Visibility);
        }
    }
}
