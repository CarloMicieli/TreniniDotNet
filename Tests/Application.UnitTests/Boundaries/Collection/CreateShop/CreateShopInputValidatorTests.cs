using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateShop
{
    public class CreateShopInputValidatorTests
    {
        private CreateShopInputValidator Validator { get; }

        public CreateShopInputValidatorTests()
        {
            Validator = new CreateShopInputValidator();
        }
    }
}
