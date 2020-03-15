using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugInput : IUseCaseInput
    {
        private readonly Slug _slug;

        public GetCatalogItemBySlugInput(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}
