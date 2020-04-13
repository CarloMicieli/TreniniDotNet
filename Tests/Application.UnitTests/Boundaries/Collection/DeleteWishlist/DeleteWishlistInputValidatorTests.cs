using FluentValidation.TestHelper;
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
    }
}
