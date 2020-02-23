using FluentValidation;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug
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
