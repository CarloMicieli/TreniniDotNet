using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateWishlist
{
    public class CreateWishlistInputValidatorTests
    {
        private CreateWishlistInputValidator Validator { get; }

        public CreateWishlistInputValidatorTests()
        {
            Validator = new CreateWishlistInputValidator();
        }
    }
}
