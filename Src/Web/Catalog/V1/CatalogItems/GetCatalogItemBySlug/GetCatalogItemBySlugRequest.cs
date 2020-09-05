using MediatR;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugRequest : IRequest
    {
        public GetCatalogItemBySlugRequest(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}