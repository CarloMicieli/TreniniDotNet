using FluentValidation;
using TreniniDotNet.Common.Validation;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public sealed class CreateBrandInputValidator : AbstractValidator<CreateBrandInput>
    {
        public CreateBrandInputValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.CompanyName)
                .MaximumLength(250);

            RuleFor(x => x.EmailAddress)
                .EmailAddress();

            RuleFor(x => x.WebsiteUrl)
                .AbsoluteUri();

            RuleFor(x => x.Kind)
                .IsEnumName(typeof(BrandKind), caseSensitive: false);
        }
    }
}
