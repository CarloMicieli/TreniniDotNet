using MediatR;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
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
