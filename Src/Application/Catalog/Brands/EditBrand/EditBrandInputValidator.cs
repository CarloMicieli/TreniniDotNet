using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public sealed class EditBrandInputValidator : AbstractUseCaseValidator<EditBrandInput>
    {
        public EditBrandInputValidator()
        {
            RuleFor(x => x.BrandSlug)
                .ValidSlug();

            RuleFor(x => x.Values)
                .SetValidator(new ModifiedBrandValuesValidator());
        }
    }
}
