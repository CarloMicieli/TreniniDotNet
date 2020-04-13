using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist
{
    public class RemoveItemFromWishlistInputValidatorTests
    {
        private RemoveItemFromWishlistInputValidator Validator { get; }

        public RemoveItemFromWishlistInputValidatorTests()
        {
            Validator = new RemoveItemFromWishlistInputValidator();
        }
    }
}
