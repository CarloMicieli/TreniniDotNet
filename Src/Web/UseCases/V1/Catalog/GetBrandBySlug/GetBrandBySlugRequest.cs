using MediatR;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    public class GetBrandBySlugRequest : IRequest
    {
        private readonly Slug _slug;

        public GetBrandBySlugRequest(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}
