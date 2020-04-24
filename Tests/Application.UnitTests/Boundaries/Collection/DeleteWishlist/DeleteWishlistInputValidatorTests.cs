using FluentValidation.TestHelper;
using System;
using TreniniDotNet.Application.TestInputs.Collection;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist
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
            var input = CollectionInputs.DeleteWishlist.With();

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void DeleteWishlistInputValidator_ShouldValidate_ValidInputs()
        {
            var input = CollectionInputs.DeleteWishlist.With(Id: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
