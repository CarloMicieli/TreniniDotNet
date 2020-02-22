using FluentValidation;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
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
