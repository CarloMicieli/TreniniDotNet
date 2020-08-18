using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public class GetBrandBySlugInput : IUseCaseInput
    {
        public GetBrandBySlugInput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
