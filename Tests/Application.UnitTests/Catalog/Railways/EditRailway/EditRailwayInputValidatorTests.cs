using FluentValidation.TestHelper;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public class EditRailwayInputValidatorTests
    {
        private EditRailwayInputValidator Validator { get; }

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
                railwaySlug: Slug.Of("RhB"),
                country: "ZZ");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.Country);
        }
    }
}
