using MediatR;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandBySlug
{
    public class GetBrandBySlugRequest : IRequest
    {
        public GetBrandBySlugRequest(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
