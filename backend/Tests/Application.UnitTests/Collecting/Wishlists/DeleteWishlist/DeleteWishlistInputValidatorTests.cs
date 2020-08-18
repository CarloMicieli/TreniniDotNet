using System;
using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public class DeleteWishlistInputValidatorTests
    {
        private DeleteWishlistInputValidator Validator { get; }

        public DeleteWishlistInputValidatorTests()
        {
            Validator = new DeleteWishlistInputValidator();
        }

        [Fact]
        public void DeleteWishlistInputValidator_ShouldFail_WithEmptyInputs()
        {
            var input = NewDeleteWishlistInput.With();

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void DeleteWishlistInputValidator_ShouldValidate_ValidInputs()
        {
            var input = NewDeleteWishlistInput.With(id: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
