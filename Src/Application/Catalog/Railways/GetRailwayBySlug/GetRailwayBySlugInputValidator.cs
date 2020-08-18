using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugInputValidator : AbstractUseCaseValidator<GetRailwayBySlugInput>
    {
        public GetRailwayBySlugInputValidator()
        {
            RuleFor(x => x.Slug)
                .ValidSlug();
        }
    }
}
