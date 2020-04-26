using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugInput : IUseCaseInput
    {
        public GetCatalogItemBySlugInput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
