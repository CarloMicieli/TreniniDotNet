using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public sealed class GetScaleBySlugInputValidator : AbstractUseCaseValidator<GetScaleBySlugInput>
    {
        public GetScaleBySlugInputValidator()
        {
            RuleFor(x => x.Slug)
                .ValidSlug();
        }
    }
}
