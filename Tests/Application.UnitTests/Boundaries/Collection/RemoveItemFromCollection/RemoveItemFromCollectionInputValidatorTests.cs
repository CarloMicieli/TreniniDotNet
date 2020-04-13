using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection
{
    public class RemoveItemFromCollectionInputValidatorTests
    {
        private RemoveItemFromCollectionInputValidator Validator { get; }

        public RemoveItemFromCollectionInputValidatorTests()
        {
            Validator = new RemoveItemFromCollectionInputValidator();
        }
    }
}
