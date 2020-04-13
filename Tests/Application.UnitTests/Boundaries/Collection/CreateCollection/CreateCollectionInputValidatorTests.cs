using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateCollection
{
    public class CreateCollectionInputValidatorTests
    {
        private CreateCollectionInputValidator Validator { get; }

        public CreateCollectionInputValidatorTests()
        {
            Validator = new CreateCollectionInputValidator();
        }
    }
}
