using FluentValidation.TestHelper;
using TreniniDotNet.Domain.Pagination;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.GetShopsList
{
    public class GetShopsListInputValidatorTests
    {
        private GetShopsListInputValidator Validator { get; }

        public GetShopsListInputValidatorTests()
        {
            Validator = new GetShopsListInputValidator();
        }

        [Fact]
        public void GetShopsListInputValidator_ShouldValidateInput()
        {
            var result = Validator.TestValidate(new GetShopsListInput(new Page(0, 10)));
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
