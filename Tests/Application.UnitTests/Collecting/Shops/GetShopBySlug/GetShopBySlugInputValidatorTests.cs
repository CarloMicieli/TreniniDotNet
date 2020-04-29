using FluentValidation.TestHelper;
using TreniniDotNet.Common;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public class GetShopBySlugInputValidatorTests
    {
        private GetShopBySlugInputValidator Validator { get; }

        public GetShopBySlugInputValidatorTests()
        {
            Validator = new GetShopBySlugInputValidator();
        }

        [Fact]
        public void GetShopBySlugInputValidator_ShouldValidateInput()
        {
            var result = Validator.TestValidate(new GetShopBySlugInput(Slug.Of("valid")));
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
