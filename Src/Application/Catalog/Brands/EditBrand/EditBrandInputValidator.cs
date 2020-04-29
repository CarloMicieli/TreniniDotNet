using FluentValidation;
using TreniniDotNet.Common.Validation;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
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
