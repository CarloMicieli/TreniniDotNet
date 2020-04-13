using FluentValidation.TestHelper;
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
    }
}
