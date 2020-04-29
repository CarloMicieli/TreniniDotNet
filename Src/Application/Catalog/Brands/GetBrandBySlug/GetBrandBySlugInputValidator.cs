using FluentValidation;
using TreniniDotNet.Common.Validation;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public sealed class GetBrandBySlugInputValidator : AbstractValidator<GetBrandBySlugInput>
    {
        public GetBrandBySlugInputValidator()
        {
            RuleFor(x => x.Slug)
                .ValidSlug();
        }
    }
}
