using FluentValidation;
using TreniniDotNet.Common.Validation;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
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
