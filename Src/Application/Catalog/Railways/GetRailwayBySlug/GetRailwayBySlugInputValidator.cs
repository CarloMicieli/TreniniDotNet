using FluentValidation;
using TreniniDotNet.Common.Validation;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
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
