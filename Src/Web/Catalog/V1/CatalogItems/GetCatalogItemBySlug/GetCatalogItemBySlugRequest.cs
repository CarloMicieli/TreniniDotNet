using MediatR;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugRequest : IRequest
    {
        private readonly Slug _slug;

        public GetCatalogItemBySlugRequest(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}