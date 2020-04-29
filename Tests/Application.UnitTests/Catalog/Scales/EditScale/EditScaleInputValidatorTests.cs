using FluentValidation.TestHelper;
using TreniniDotNet.Common;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public class EditScaleInputValidatorTests
    {
        private readonly EditScaleInputValidator Validator;

        public EditScaleInputValidatorTests()
        {
            Validator = new EditScaleInputValidator();
        }

        [Fact]
        public void EditScaleInputValidator_ShouldFailToValidateEmptyInputs()
        {
            var result = Validator.TestValidate(NewEditScaleInput.Empty);
            result.ShouldHaveValidationErrorFor(x => x.ScaleSlug);
        }

        [Fact]
        public void EditScaleInputValidator_ShouldRequireOnlyScaleSlug()
        {
            var result = Validator.TestValidate(NewEditScaleInput.With(ScaleSlug: Slug.Of("RhB")));
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void EditScaleInputValidator_ShouldValidatedModifiedValues()
        {
            var input = NewEditScaleInput.With(
                ScaleSlug: Slug.Of("RhB"),
                Ratio: -10M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.Ratio);
        }
    }
}
