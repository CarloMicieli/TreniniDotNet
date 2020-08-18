using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public sealed class GetBrandBySlugInputValidator : AbstractUseCaseValidator<GetBrandBySlugInput>
    {
        public GetBrandBySlugInputValidator()
        {
            RuleFor(x => x.Slug)
                .ValidSlug();
        }
    }
}
