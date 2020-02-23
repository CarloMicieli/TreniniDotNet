using FluentValidation;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug
{
    public sealed class GetScaleBySlugInputValidator : AbstractValidator<GetScaleBySlugInput>
    {
        public GetScaleBySlugInputValidator()
        {
            RuleFor(x => x.Slug)
                .ValidSlug();
        }
    }
}
