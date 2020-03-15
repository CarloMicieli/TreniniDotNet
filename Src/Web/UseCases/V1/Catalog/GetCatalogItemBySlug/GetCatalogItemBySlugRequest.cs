using MediatR;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetCatalogItemBySlug
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