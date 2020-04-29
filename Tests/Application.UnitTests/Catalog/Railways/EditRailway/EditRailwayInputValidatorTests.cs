using FluentValidation.TestHelper;
using TreniniDotNet.Common;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public class EditRailwayInputValidatorTests
    {
        private readonly EditRailwayInputValidator Validator;

        public EditRailwayInputValidatorTests()
        {
            Validator = new EditRailwayInputValidator();
        }

        [Fact]
        public void EditRailwayInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewEditRailwayInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.RailwaySlug);
        }

        [Fact]
        public void EditRailwayInput_ShouldFailValidation_WhenCountryCodeIsNotValid()
        {
            var input = NewEditRailwayInput.With(
                RailwaySlug: Slug.Of("RhB"),
                Country: "ZZ");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.Country);
        }
    }
}
