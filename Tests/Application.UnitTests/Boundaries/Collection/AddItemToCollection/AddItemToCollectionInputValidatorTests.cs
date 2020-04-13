using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection
{
    public class AddItemToCollectionInputValidatorTests
    {
        private AddItemToCollectionInputValidator Validator { get; }

        public AddItemToCollectionInputValidatorTests()
        {
            Validator = new AddItemToCollectionInputValidator();
        }
    }
}
