using FluentValidation;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugInputValidator : AbstractValidator<GetRailwayBySlugInput>
    {
        public GetRailwayBySlugInputValidator()
        {
            RuleFor(x => x.Slug)
                .ValidSlug();
        }
    }
}
