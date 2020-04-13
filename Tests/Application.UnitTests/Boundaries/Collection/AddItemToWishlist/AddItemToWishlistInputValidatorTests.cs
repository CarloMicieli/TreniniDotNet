using FluentValidation.TestHelper;
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
    }
}
