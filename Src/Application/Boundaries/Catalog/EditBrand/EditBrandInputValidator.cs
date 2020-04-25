using FluentValidation;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditBrand
{
    public sealed class EditBrandInputValidator : AbstractValidator<EditBrandInput>
    {
        public EditBrandInputValidator()
        {
            RuleFor(x => x.BrandSlug)
                .ValidSlug();

            RuleFor(x => x.Values)
                .SetValidator(new ModifiedBrandValuesValidator());
        }
    }

    internal sealed class ModifiedBrandValuesValidator : AbstractValidator<ModifiedBrandValues>
    {
        public ModifiedBrandValuesValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50);

            RuleFor(x => x.CompanyName)
                .MaximumLength(250);

            RuleFor(x => x.EmailAddress)
                .EmailAddress();

            RuleFor(x => x.WebsiteUrl)
                .AbsoluteUri();

            RuleFor(x => x.BrandType)
                .IsEnumName(typeof(BrandKind), caseSensitive: false);
        }
    }
}
