using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem
{
    public class EditCollectionItemInputValidatorTests
    {
        private EditCollectionItemInputValidator Validator { get; }

        public EditCollectionItemInputValidatorTests()
        {
            Validator = new EditCollectionItemInputValidator();
        }
    }
}
